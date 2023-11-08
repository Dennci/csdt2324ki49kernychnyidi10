#include <Arduino.h>
#include <TinyXML.h>

const int buzzerPin = 11; 
bool isAlarmActive = false;
String receivedData = "";

void setup() {
  pinMode(buzzerPin, OUTPUT);
  digitalWrite(buzzerPin, LOW); 
  Serial.begin(9600);
}

void loop() {
  if (Serial.available() > 0) {
    receivedData = Serial.readString();

    // Print received XML data
    Serial.print("Received XML: ");
    Serial.println(receivedData);

    // Simple XML parsing
    int start_pos = receivedData.indexOf("<AlertNow>");
    int end_pos = receivedData.indexOf("</AlertNow>");

    if (start_pos != -1 && end_pos != -1) {
      String alertNow = receivedData.substring(start_pos + 10, end_pos); // Extract the value between <AlertNow> and </AlertNow>
      Serial.print("Extracted AlertNow: ");
      Serial.println(alertNow);
      
      // Activate or deactivate alarm based on AlertNow value
      if (alertNow == "true") {
        activateAlarm();
      } else {
        deactivateAlarm();
      }
    }
  }
}


void activateAlarm() {
  isAlarmActive = true;
 //16s for alarm
  for (int i = 0; i < 8; i++) {
    tone(buzzerPin, 600); 
    delay(1000);        
    noTone(buzzerPin);
    delay(1000);         
  }
  noTone(buzzerPin);
  isAlarmActive = false; 
}

void deactivateAlarm() {
  isAlarmActive = true;
 //5s for allarm
  for (int i = 0; i < 5; i++) {
    tone(buzzerPin, 750); 
    delay(1500);        
    noTone(buzzerPin);
    delay(1500);         
  }
  noTone(buzzerPin);
  isAlarmActive = false; 
}
