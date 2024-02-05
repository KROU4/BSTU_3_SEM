import string

message = "Велютич Дмитрий Игоревич"
key = "17ЗАЩИТА"
key = "".join([char for char in key if char.isdigit()])

rus_letters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя"

encrypted = ""
for char in message:
  if char in rus_letters:
    encrypted += rus_letters[(rus_letters.index(char) + 3) % len(rus_letters)]  
  else:
    encrypted += char
print("Цезарь:", encrypted)

encrypted = ""
for char in message:
  if char in rus_letters:
    encrypted += chr((ord(char) + 3 - ord('А')) % len(rus_letters) + ord('А'))
  else:  
    encrypted += char   
print("Трисемус:", encrypted)

encrypted = ""  
for i, char in enumerate(message):
  if char in rus_letters:   
    encrypted += chr((ord(char) + int(key[i%len(key)])) % len(rus_letters) + ord('А'))
  else:
    encrypted += char
print("Плейфейр:", encrypted) 

key_num = [ord(x)-ord('А') for x in key]  
encrypted = ""
for i, char in enumerate(message):
  if char in rus_letters:
    encrypted += chr((ord(char) + key_num[i%len(key_num)]) % len(rus_letters) + ord('А')) 
  else:
    encrypted += char
print("Вижинер:", encrypted)