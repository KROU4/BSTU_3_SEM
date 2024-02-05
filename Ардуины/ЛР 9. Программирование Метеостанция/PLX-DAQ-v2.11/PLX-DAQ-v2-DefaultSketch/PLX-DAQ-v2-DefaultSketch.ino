#define THERMISTOR_PIN A0
#define SERIES_RESISTOR 10000.0 // Значение резистора, подключенного последовательно с термистором (10K)
#define NOMINAL_RESISTANCE 100000.0 // Номинальное сопротивление термистора (100K)
#define B_COEFFICIENT 3950.0       // B-коэффициент для вашего термистора

void setup() {
  // Initialize the serial connection
  Serial.begin(9600);

  // Rest of your setup code...
  Serial.println("CLEARSHEET");
  Serial.println("LABEL,Date,Time,Timer,Counter,millis,Temperature");
  Serial.println("CUSTOMBOX1,LABEL,Stop logging at 250?");
  Serial.println("CUSTOMBOX2,LABEL,Resume log at 350?");
  Serial.println("CUSTOMBOX3,LABEL,Quit at 450?");
  Serial.println("CUSTOMBOX1,SET,1");
  Serial.println("CUSTOMBOX2,SET,1");
  Serial.println("CUSTOMBOX3,SET,0");
}

void loop() {
  // Read analog value from the thermistor
  int i = 0; // Объявление переменной i
  int rawValue = analogRead(THERMISTOR_PIN);

  // Calculate resistance of the thermistor
  float voltage = (float)rawValue / 1023.0 * 5.0; // Assuming 5V reference voltage
  float resistance = SERIES_RESISTOR / ((5.0 / voltage) - 1.0);

  // Calculate temperature using Steinhart-Hart equation
  float temperatureK = 1.0 / ((log(resistance / NOMINAL_RESISTANCE) / B_COEFFICIENT) + (1.0 / 298.15));
  float temperatureC = temperatureK - 273.15;

  // Send data to Excel via PLX-DAQ
  Serial.print("DATA,DATE,TIME,TIMER,");
  Serial.print(i++);
  Serial.print(",");
  Serial.print(millis());
  Serial.print(",AUTOSCROLL_20,");
  Serial.print(temperatureC);
  Serial.println(",Temperature");

  // Rest of your loop code...

  // Clear some cells in Excel (rectangle range from B10 to D20)
  if (i == 100)
    Serial.println("ClearRange,B,10,D,20");

  // Do a simple beep in Excel on PC
  if (i == 150)
    Serial.println("BEEP");

  // Read a value from Excel
  if (i == 200) {
    Serial.println("CELL,GET,FROMSHEET,Simple Data,E,4");
    int readvalue = Serial.readStringUntil(10).toInt();
    Serial.println("Value of cell E4 is: " + String(readvalue));
  }

  // Check value of custombox1 on PLX DAQ in Excel
  if (i == 250) {
    Serial.println("CUSTOMBOX1,GET");
    int stoplogging = Serial.readStringUntil(10).toInt();
    Serial.println("Value of stoplogging/checkbox is: " + String(stoplogging));
    if (stoplogging)
      Serial.println("PAUSELOGGING");
  }

  // Get a true random number from the computer
  if (i == 300) {
    Serial.println("GETRANDOM,-4321,12345");
    int rndseed = Serial.readStringUntil(10).toInt();
    Serial.println("Got random value '" + String(rndseed) + "' from Excel");
  }

  // Resume logging
  if (i == 350) {
    Serial.println("CUSTOMBOX2,GET");
    int resumelogging = Serial.readStringUntil(10).toInt();
    if (resumelogging)
      Serial.println("RESUMELOGGING");
  }

  // Post to specific cells on default sheet as well as named sheet
  if (i == 400) {
    Serial.println("CELL,SET,G10,400 test 1 string");
    Serial.println("CELL,SET,ONSHEET,Simple Data,G,11,400 test 2 string");
  }

  // Forced quit of Excel with saving the file first
  if (i == 450) {
    Serial.println("CUSTOMBOX3,GET");
    if (Serial.readStringUntil(10).toInt()) {
      Serial.println("SAVEWORKBOOKAS,450-Lines-File");
      Serial.println("FORCEEXCELQUIT");
    } else
      Serial.println("No forced Excel quit requested!");
  }

  // Ваш остальной код loop...

  // Увеличение i для следующей итерации
  i++;
}