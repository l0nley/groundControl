// Example testing sketch for various DHT humidity/temperature sensors
// Written by ladyada, public domain

#include "DHT.h"

#define DHTPIN 7     // what pin we're connected to

// Uncomment whatever type you're using!
#define DHTTYPE DHT11   // DHT 11 
//#define DHTTYPE DHT22   // DHT 22  (AM2302)
//#define DHTTYPE DHT21   // DHT 21 (AM2301)

#define PWM_A 3
#define PWM_B 11
#define SNS_A A0
#define SNS_B A1
#define BREAK_A 9
#define BREAK_B 8
#define DIR_A 12
#define DIR_B 13
#define BUZZER 2


#define ECHO 5
#define TRIGER 4

#define RED A2
#define GREEN A3
#define BLUE A4
#define LED 6

// Connect pin 1 (on the left) of the sensor to +5V
// Connect pin 2 of the sensor to whatever your DHTPIN is
// Connect pin 4 (on the right) of the sensor to GROUND
// Connect a 10K resistor from pin 2 (data) to pin 1 (power) of the sensor

DHT dht(DHTPIN, DHTTYPE);

typedef union {
 float floatingPoint;
 byte binary[4];
} binaryFloat;

typedef union {
  int intPoint;
  byte binary[2];
} binaryInt;

byte motorSpeed;
byte ar;
byte br;

void setup() {  
  motorSpeed = 0;
  ar = 0;
  br = 0;
  Serial.begin(9600); 
  pinMode(TRIGER, OUTPUT); 
  pinMode(ECHO, INPUT); 
  pinMode(RED, OUTPUT);
  pinMode(GREEN, OUTPUT);
  pinMode(BLUE, OUTPUT);
  pinMode(LED, OUTPUT);
  pinMode(BUZZER, OUTPUT);
  pinMode(BREAK_A, OUTPUT);
  pinMode(BREAK_B, OUTPUT);
  pinMode(DIR_A, OUTPUT);
  pinMode(DIR_B, OUTPUT);
  
  dht.begin();
}

void a_stop() {
  digitalWrite(BREAK_A,HIGH);
  analogWrite(PWM_A,0);
  ar = 0;
}

void a_run(int dir) {
  digitalWrite(BREAK_A,LOW);
  if(dir == 1)
  {
    digitalWrite(DIR_A, LOW);
  } else {
    digitalWrite(DIR_A, HIGH);
  }
  analogWrite(PWM_A, motorSpeed);
  ar = 1;
}

void b_stop(){
  digitalWrite(BREAK_B,HIGH);
  analogWrite(PWM_B,0);
  br=0;
}

void b_run(int dir) {
  digitalWrite(BREAK_B,LOW);
  if(dir == 1)
  {
    digitalWrite(DIR_B, LOW);
  } else {
    digitalWrite(DIR_B, HIGH);
  }
  analogWrite(PWM_B, motorSpeed);
  br=1;
}

void forward() {
    b_run(1);
    a_run(0);
}

void backward() {
   b_run(0);
   a_run(1);
}

void right() {
   a_stop();
   b_run(1);
}

void left() {
    b_stop();
    a_run(0);
}

void stopMotors() {
    a_stop();
    b_stop();
}



void loop() {
  // Reading temperature or humidity takes about 250 milliseconds!
  // Sensor readings may also be up to 2 seconds 'old' (its a very slow sensor)
  binaryFloat h;
  h.floatingPoint = dht.readHumidity();
  binaryFloat t;
  t.floatingPoint= dht.readTemperature();
  binaryFloat duration; 
  binaryFloat cm;
  digitalWrite(TRIGER, LOW); 
  delayMicroseconds(2); 
  digitalWrite(TRIGER, HIGH); 
  delayMicroseconds(10); 
  digitalWrite(TRIGER, LOW); 
  duration.floatingPoint = pulseIn(ECHO, HIGH); 
  cm.floatingPoint = duration.floatingPoint / 58.0;
  char val;
  if(cm.floatingPoint <= 0.0) {
    digitalWrite(RED,LOW);
    digitalWrite(GREEN,LOW);
    digitalWrite(BLUE,LOW);
  }
  else if(cm.floatingPoint < 10.0) {
    digitalWrite(RED,HIGH);
    digitalWrite(GREEN,LOW);
    digitalWrite(BLUE,LOW);
  } else if (cm.floatingPoint < 50.0) {
    digitalWrite(RED,LOW);
    digitalWrite(GREEN,HIGH);
    digitalWrite(BLUE,LOW);
  } else {
    digitalWrite(RED,LOW);
    digitalWrite(GREEN,LOW);
    digitalWrite(BLUE,HIGH);
  }
  
  
  // check if returns are valid, if they are NaN (not a number) then something went wrong!
  if (isnan(t.floatingPoint) || isnan(h.floatingPoint)) {
    t.floatingPoint = 0.0;
    h.floatingPoint = 0.0;
  } 

  binaryInt a;
  binaryInt b;
  a.intPoint = analogRead(SNS_A);
  b.intPoint = analogRead(SNS_B);

  Serial.write((byte)'H');
  Serial.write(h.binary,4);
  Serial.write((byte)'T');
  Serial.write(t.binary, 4);
  Serial.write((byte)'D');
  Serial.write(cm.binary,4);
  Serial.write((byte)'A');
  Serial.write(a.binary,2);
  Serial.write((byte)'B');
  Serial.write(b.binary,2);
  Serial.write((byte)'S');
  Serial.write((byte)motorSpeed);
  
  if( Serial.available() )       // if data is available to read
  {
    val = Serial.read();         // read it and store it in 'val'
  }
  if( val == 'H' )               // if 'H' was received
  {
    digitalWrite(LED, HIGH);  // turn ON the LED\
    delayMicroseconds(100);
  }
  if(val == '6')
  {
    right();
  }
  if(val == '4')
  {
    left();
  }
  if(val == '8')
  {
    forward();
  }
   if(val == '2')
  {
    backward();
  }
  if(val == '5')
  {
    stopMotors();
  }

  if(val == '+')
  {
    if(motorSpeed < 224)
    {
      motorSpeed = motorSpeed + 32;  
    } else {
      motorSpeed = 255;
    }

    if(ar>0){
      analogWrite(PWM_A, motorSpeed);
    }
    if(br>0) {
      analogWrite(PWM_B, motorSpeed);
    }
  }

  if(val == '-')
  {
    if(motorSpeed > 32) 
    {
      motorSpeed = motorSpeed - 32;
    }
    else 
    {
      motorSpeed = 0;
    }
    
    if(ar>0){
      analogWrite(PWM_A, motorSpeed);
    }
    if(br>0) {
      analogWrite(PWM_B, motorSpeed);
    }
  }
  
  digitalWrite(BUZZER, HIGH);
  delayMicroseconds(1915);
  digitalWrite(BUZZER, LOW);
  digitalWrite(LED, LOW);   // otherwise turn it OFF
}
