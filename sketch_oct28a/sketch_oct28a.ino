#include <Arduino.h>
#include <TinyXML.h>

// Global Variables
const int buzzerPin = 11; ///< we declare the pin to which the speaker is connected
bool isAlarmActive = false;///< status for alarm system

String receivedData = ""; 


/**
 *
 * Execution of the program starts here.
 * This function setting one time arduino
 * zero params
 * 
 */
void setup() {
  pinMode(buzzerPin, OUTPUT);
  digitalWrite(buzzerPin, LOW); 
  Serial.begin(9600);
}

/**
 *
 * This function infinity loop code(if arduino has power on)
 * 
 */
void loop() {
  ///< Check if data is available to read through the serial port. If data is available (greater than zero bytes), it is read
  if (Serial.available() > 0) {
    receivedData = Serial.readString();

    Serial.print("Received XML: ");
    Serial.println(receivedData);
    ///< It is trying to extract some information, probably an element named <AlertNow> and its contents.
    int start_pos = receivedData.indexOf("<AlertNow>");
    int end_pos = receivedData.indexOf("</AlertNow>");

    if (start_pos != -1 && end_pos != -1) {
      String alertNow = receivedData.substring(start_pos + 10, end_pos); 
      Serial.print("Extracted AlertNow: ");
      Serial.println(alertNow);
      
      
      if (alertNow == "true") {
        activateAlarm();
      } else {
        deactivateAlarm();
      }
    }
  }
}

/**
 *
 * This function activate alarm for announcement of air alert
 * 
 */
void activateAlarm() {
  isAlarmActive = true;
 // activate alarm for 16s for alarm
  for (int i = 0; i < 8; i++) {
    tone(buzzerPin, 600); 
    delay(1000);        
    noTone(buzzerPin);
    delay(1000);         
  }
  noTone(buzzerPin);
  isAlarmActive = false; 
}
/**
 *
 * advertisement air alarm cancellation 
 * 
 */
void deactivateAlarm() {
  isAlarmActive = true;
  // activate alarm for 10s for alarm
  for (int i = 0; i < 5; i++) {
    tone(buzzerPin, 750); 
    delay(1500);        
    noTone(buzzerPin);
    delay(1500);         
  }
  noTone(buzzerPin);
  isAlarmActive = false; 
}
