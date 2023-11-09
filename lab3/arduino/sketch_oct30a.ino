String mySt="";
boolean stringComplete = false;
void setup() {
  // put your setup code here, to run once:
Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  if(Serial.available()){
  mySt= Serial.readString();
Serial.print("Hello world From Denys");
  }
  
  mySt=""; 
}