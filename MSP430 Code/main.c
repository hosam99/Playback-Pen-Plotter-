#include <msp430.h> 
#define GLOBAL_IQ  11
#include "IQmathLib.h"

const char packetSize = 7, databufferSize = 35, stepnum = 8;
volatile signed char packetindex = 0, index = -1, activeindex = 0;
volatile signed char positionindex = 0, positionactiveindex = 0;
volatile unsigned char bufferLength = 0, positionbufferLength = 0, datathresh = 0, datacounter = 0;
volatile unsigned char positionbuffer[databufferSize][6];
volatile unsigned char buffer[databufferSize][packetSize];
volatile unsigned char x_dir, y_dir, penStatus, x_stepindex, y_stepindex, minthresh = 5;
volatile unsigned int x_setpoint = 0, y_setpoint = 0, offset = 30000, x_zero = 0, y_zero = 0;
volatile unsigned int x_stepcounts = 0, y_stepcounts = 0, stepperDelay = 1000;
volatile unsigned int x_error, y_error, xsteps = 0, ysteps = 0;
volatile unsigned int stepstaken = 0, accelsteps = 0, result, prevdelay;
volatile unsigned char xpositionReached = 1, ypositionReached = 1, positionReached = 1, processingData = 0, realTime = 0;
 volatile unsigned char processing = 0, state = 0, x_waiting = 0, y_waiting = 0, zeroing = 0;
volatile unsigned char x_stepSequence[stepnum] = {0b00001000, 0b00011000, 0b00010000, 0b00110000, 0b00100000, 0b01100000, 0b01000000, 0b01001000};
volatile unsigned char y_stepSequence[stepnum] = {0b00000001, 0b00000011, 0b00000010, 0b00000110, 0b00000100, 0b00001100, 0b00001000, 0b00001001};

int main(void)
{

   WDTCTL = WDTPW | WDTHOLD;               // stop watchdog timer
    CSCTL0_H = 0xA5;                        // Writing CSKEY Password
    CSCTL1 = DCOFSEL_1 + DCORSEL;           // Set max. DCO setting = 20MHz
    CSCTL2 = SELA_3 + SELS_3 + SELM_3;      // MCLK = ACLK = SMCLK = DCO
    CSCTL3 = DIVA_1 + DIVS_0 + DIVM_0;      // set dividers ACLK/1, SMCLK/1, MCLK/1

    P1DIR |= BIT3 + BIT4 + BIT5 + BIT6;     // P1.3 P1.4, P1.5 P1.6 Outputs
    P3DIR |= BIT0 + BIT1 + BIT2 + BIT3;     // P3.0 - 3.3 Outputs
    P2SEL0 &= ~(BIT0 + BIT1);               // Configure P2.0, P2.1 as RX and TX
    P2SEL1 |= BIT0 + BIT1;                  // Configure P2.0, P2.1 as RX and TX
    P1DIR |=  BIT2;                         // P1.2 output (TA1.1)
    P1SEL0 |= BIT2;                         // P1.2 options select (TA1.1)
    P3OUT &= ~(BIT0 + BIT1 + BIT2 + BIT3);  // Clear all output bits
    P1OUT &= ~(BIT3 + BIT4 + BIT5 + BIT6);  // Clear all output bits

    UCA0CTLW0 |= UCSWRST;                   // Set UCSWRST before configuring UART registers
    UCA0CTLW0 |= UCSSEL__SMCLK;             // Set clock source as SMCLK
    UCA0BRW = 130;                          // Configuring 9600 Baud Rate for 20 MHz CLK
    UCA0MCTLW = 0x2500 + UCOS16 + UCBRF_3;  // Configuring 9600 Baud Rate for 20 MHz CLK
    UCA0CTLW0 &= ~UCSWRST;                  // Clearing UCSWRST releases the UART for operation.

    unsigned int period = 50000;

    TB0CCR0 = 0;                                    // TB0 Delay timer for x axis stepper
    TB1CCR0 = 0;                                    // TB1 Delay timer for y axis stepper
    TA1CCR0 = period;                               // 20 ms PWM Period
    TA1CCTL1 = OUTMOD_7;                            // CCR1 reset/set
    TA1CCR1 = 0;                                    // 0 % Duty cycle for Servo
    TB0CCTL0 |= CCIE;                               // Enable TB0 CCR0 interrupt
    TB1CCTL0 |= CCIE;                               // Enable TB1 CCR0 interrupt
    TB0EX0 |= TBIDEX_4;                             // Set clock divider to 5
    TB1EX0 |= TBIDEX_4;                             // Set clock divider to 5
    TB0CTL = TBSSEL__ACLK + MC__UP + ID_1 + TBCLR;      // ACLK,  Up mode, clear TBR, total divider = 10 (1MHz)
    TB1CTL = TBSSEL__ACLK + MC__UP + ID_1 + TBCLR;      // ACLK,  Up mode, clear TBR, total divider = 10 (1MHz)
    TA1CTL = TASSEL__SMCLK + MC__UP + ID_3 + TACLR;    // TACLK, Continuous mode, clear TAR

    UCA0IE |= UCRXIE;                       // Enable Receive Interrupt
    _EINT();                                // Global interrupt enable

    while(1){
        while(bufferLength > 0){ processData();}
        if(positionbufferLength > 0){ processPositionData();}
        controlLoop();
    };
}

#pragma vector = USCI_A0_VECTOR
__interrupt void USCI_A0_ISR(void)
{
    unsigned char RxByte = UCA0RXBUF;       // Retrieve data from receive buffer

    if(RxByte == 255 && processing == 0){   // Check if 255 was received, if so start storing data in nested array
        processing = 1;
        if(index == databufferSize - 1) {index = 0;}
        else{index++;}
        buffer[index][packetindex] = RxByte;
        packetindex++;
    }else if(processing == 1){
        buffer[index][packetindex] = RxByte;
        if(packetindex == packetSize - 1 ) {packetindex = 0; bufferLength++; processing = 0;}
        else{packetindex++;}
    }

}

void processData(){

    unsigned char commandByte = buffer[activeindex][1];
    unsigned int upperDataByte_x = buffer[activeindex][2];
    unsigned int lowerDataByte_x = buffer[activeindex][3];
    unsigned int upperDataByte_y = buffer[activeindex][4];
    unsigned int lowerDataByte_y = buffer[activeindex][5];
    unsigned char penstatusByte = buffer[activeindex][6];
    unsigned int xdata = ((upperDataByte_x << 8) + lowerDataByte_x);
    unsigned int ydata = ((upperDataByte_y << 8) + lowerDataByte_y);

    if(commandByte < 5){   // Indicates that the next command is a position command

        state = 1;
        unsigned char i = 0;

        while(i < 6){
            if(i == 5){ positionbuffer[positionindex][i] = buffer[activeindex][1];}
            else{ positionbuffer[positionindex][i] = buffer[activeindex][i+2]; }
            i++;
        }

        datacounter++;
        if(datacounter < datathresh){processingData = 1;}
        else{processingData = 0; datacounter = 0;}

        if(positionindex == databufferSize - 1 ) {positionindex = 0;}
        else{positionindex++;}
        positionbufferLength++;

    }else{

        state = 0;

        switch(commandByte)
        {
            case 5:
                if(penStatus == 1){penStatus = 0;}
                else{penStatus = 1;}
                break;

           case 6 :
              stepperDelay = xdata;
              break;

           case 9 :
              x_dir = 1;
              break;

           case 10 :
              x_dir = 2;
              break;

           case 7 :
              y_dir = 1;
              break;

           case 8 :
              y_dir = 2;
              break;

           case 11 :
              x_dir = 0;
              y_dir = 0;
              break;

           case 12:
               x_zero = x_stepcounts + offset;
               y_zero = y_stepcounts + offset;
               break;

           case 13:
               state = 1;
               x_setpoint = x_zero;
               y_setpoint = y_zero;
               break;

           case 14:
               datathresh = xdata;
               if(ydata == 1){minthresh = 0;}
               else{minthresh = 5;}
               break;

           case 15:
               realTime = 1;
               break;

           case 16:
               realTime = 0;
               break;
        }
    }

    if(activeindex == databufferSize - 1 ) {activeindex = 0;}
    else{activeindex++;}
    bufferLength--;

}

#pragma vector = TIMER0_B0_VECTOR
__interrupt void TB0_CCR0_ISR(void){

    x_waiting = 0;            // stops stepper delay allowing it to move to next position
    TB0CCR0 = 0;            // Reset Timer
    TB0CCTL0 &= ~CCIFG;     // Reset Flag
}

#pragma vector = TIMER1_B0_VECTOR
__interrupt void TB1_CCR0_ISR(void){

    y_waiting = 0;            // stops stepper delay allowing it to move to next position
    TB1CCR0 = 0;            // Reset Timer
    TB1CCTL0 &= ~CCIFG;     // Reset Flag
}

void controlLoop(){

    if(state == 1){     // Position Control Mode
        unsigned int x_position = x_stepcounts + offset;
        unsigned int y_position = y_stepcounts + offset;

        if(x_setpoint > x_position){ x_error = x_setpoint - x_position; x_dir = 1; }
        else if(x_setpoint < x_position){ x_error = x_position - x_setpoint; x_dir = 2; }
        else{x_dir = 0; x_error = 0; xpositionReached = 1;}

        if(y_setpoint > y_position){ y_error = y_setpoint - y_position; y_dir = 1; }
        else if(y_setpoint < y_position){ y_error = y_position - y_setpoint; y_dir = 2; }
        else{y_dir = 0; y_error = 0; ypositionReached = 1;}

        if(xpositionReached == 1 && ypositionReached == 1){
            positionReached = 1;
            xpositionReached = 0;
            ypositionReached = 0;
        }

        drawLine(x_dir, y_dir);

    }else{
        drawLine(x_dir, y_dir);
    }
}

void drawLine(char x_dir, char y_dir){

    if(penStatus == 1){ TA1CCR1 = 1100; }
    else{TA1CCR1 = 1550;}

    if(x_waiting == 0 && x_dir != 0){

        if(x_dir == 1){
            if(x_stepindex == stepnum-1){x_stepindex = 0;}
            else{x_stepindex++;}
            x_stepcounts++;
        }else if(x_dir == 2){
            if(x_stepindex == 0){x_stepindex = stepnum-1;}
            else{x_stepindex--;}
            x_stepcounts--;
        }

        P1OUT = x_stepSequence[x_stepindex];
        x_waiting = 1;

        if(state == 0){TB0CCR0 = stepperDelay;}
        else{
            if(xsteps >= ysteps){ TB0CCR0 = stepperDelay;}
            else{
                _iq qA, qB, qC, qD;
                qA = _IQ(xsteps); qB = _IQ(ysteps); qC = _IQ(stepperDelay);
                qD = _IQmpy(qC,_IQdiv(qB,qA));
                TB0CCR0 = _IQint(qD);
            }
        }
    }

    if(y_waiting == 0 && y_dir != 0){

        if(y_dir == 1){
            if(y_stepindex == stepnum-1){y_stepindex = 0;}
            else{y_stepindex++;}
            y_stepcounts++;
        }else if(y_dir == 2){
            if(y_stepindex == 0){y_stepindex = stepnum-1;}
            else{y_stepindex--;}
            y_stepcounts--;
        }

        P3OUT = y_stepSequence[y_stepindex];
        y_waiting = 1;

        if(state == 0){TB1CCR0 = stepperDelay;}
        else{
            if(ysteps >= xsteps){ TB1CCR0 = stepperDelay;}
            else{
                _iq qA, qB, qC, qD;
                qA = _IQ(xsteps); qB = _IQ(ysteps); qC = _IQ(stepperDelay);
                qD = _IQmpy(qC,_IQdiv(qA,qB));
                TB1CCR0 = _IQint(qD);
            }
        }
    }

}


void processPositionData(){
    if(positionReached == 1){     // If position reached process next data point
        positionReached = 0;
        xpositionReached = 0;
        ypositionReached = 0;

        penStatus = positionbuffer[positionactiveindex][4];
        unsigned char commandByte = positionbuffer[positionactiveindex][5];
        xsteps = ((positionbuffer[positionactiveindex][0] << 8) + positionbuffer[positionactiveindex][1]);
        ysteps = ((positionbuffer[positionactiveindex][2] << 8) + positionbuffer[positionactiveindex][3]);

        if(commandByte == 1 || commandByte == 3) { x_setpoint = x_stepcounts + offset + xsteps;}
        else {x_setpoint = x_stepcounts + offset - xsteps;}

        if(commandByte == 1 || commandByte == 2) { y_setpoint = y_stepcounts + offset + ysteps;}
        else {y_setpoint = y_stepcounts + offset - ysteps;}

        if(positionactiveindex == databufferSize - 1 ) {positionactiveindex = 0;}
        else{positionactiveindex++;}
        positionbufferLength--;

        if(positionbufferLength == minthresh && processingData == 0 && realTime == 0){IndicateReady();}

    }
}

void IndicateReady(){
    while ((UCA0IFG & UCTXIFG)==0);      // Wait until transmit buffer is empty
    UCA0TXBUF = 255;
}





