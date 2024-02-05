from Crypto.PublicKey import RSA
from Crypto.Signature import PKCS1_v1_5
from Crypto.Hash import SHA256

# Исходное сообщение
message = b"Hello, world!"
print("Исходное сообщение:", message.decode())

# Генерация ключей RSA
key = RSA.generate(2048)
private_key = key.export_key()
public_key = key.publickey().export_key()
print("Закрытый ключ:", private_key.decode())
print("Открытый ключ:", public_key.decode())

# Создание хеша сообщения
hash = SHA256.new(message)

# Создание подписи с помощью закрытого ключа RSA
signer = PKCS1_v1_5.new(key)
signature = signer.sign(hash)
print("Подпись файла:", signature.hex())

# Проверка подписи с помощью открытого ключа RSA
verifier = PKCS1_v1_5.new(key.publickey())
if verifier.verify(hash, signature):
    print("Подпись верна")
else:
    print("Подпись неверна")