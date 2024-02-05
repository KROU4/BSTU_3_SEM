const crypto = require('crypto');
const textEncoder = new TextEncoder();
const textDecoder = new TextDecoder();

async function generateRandomNumbers() {
  const randomBytes = crypto.randomBytes(16);
  console.log('Случайные числа:', new Uint8Array(randomBytes));
}

async function encryptDecryptHashName() {
  const textToEncrypt = 'Велютич';
  const data = textEncoder.encode(textToEncrypt);

  // Генерируем ключ для AES-CTR
  const aesKey = crypto.randomBytes(32);

  // Шифруем данные
  const iv = crypto.randomBytes(16);
  const cipher = crypto.createCipheriv('aes-256-ctr', aesKey, iv);
  const encryptedData = Buffer.concat([cipher.update(data), cipher.final()]);
  console.log('Зашифрованные данные:', new Uint8Array(encryptedData));

  // Дешифруем данные
  const decipher = crypto.createDecipheriv('aes-256-ctr', aesKey, iv);
  const decryptedData = Buffer.concat([decipher.update(encryptedData), decipher.final()]);
  console.log('Расшифрованные данные:', textDecoder.decode(decryptedData));

  // Хешируем данные с использованием SHA-256
  const hash = crypto.createHash('sha256').update(data).digest();
  console.log('Хеш SHA-256:', new Uint8Array(hash));
}

async function wrapUnwrapKey() {
  const aesKey = crypto.randomBytes(32);

  // Упаковываем ключ
  const wrappedKey = aesKey.toString('base64');
  console.log('Упакованный ключ:', wrappedKey);

  // Распаковываем ключ
  const unwrappedKey = Buffer.from(wrappedKey, 'base64');
  console.log('Распакованный ключ:', new Uint8Array(unwrappedKey));
}

async function signAndVerifyMessage() {
  const textToSign = 'Сообщение для подписи';

  // Генерируем ключи для RSA-PSS
  const { publicKey, privateKey } = crypto.generateKeyPairSync('rsa', {
    modulusLength: 2048,
    publicKeyEncoding: { type: 'spki', format: 'pem' },
    privateKeyEncoding: { type: 'pkcs8', format: 'pem' },
  });

  // Подписываем сообщение
  const signature = crypto.sign('sha256', textEncoder.encode(textToSign), privateKey);
  console.log('Подпись RSA-PSS:', new Uint8Array(signature));

  // Проверяем подлинность подписи
  const result = crypto.verify('sha256', textEncoder.encode(textToSign), publicKey, new Uint8Array(signature));
  console.log('Проверка подлинности:', result);
}

async function main() {
  console.log('1. Генерация случайных чисел:');
  await generateRandomNumbers();

  console.log('\n2. Шифрование, дешифрование и хеширование фамилии:');
  await encryptDecryptHashName();

  console.log('\n3. Упаковка и распаковка ключа:');
  await wrapUnwrapKey();

  console.log('\n4. Подпись сообщения и проверка подлинности:');
  await signAndVerifyMessage();
}

main();
