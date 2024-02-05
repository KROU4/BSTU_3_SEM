def double_transposition_decrypt(ciphertext, key1, key2):
  """
  Расшифровывает сообщение с помощью двойной перестановки.

  Args:
    ciphertext: Зашифрованное сообщение.
    key1: Первый ключ.
    key2: Второй ключ.

  Returns:
    Расшифрованное сообщение.
  """

  # Создаем таблицу для вписывания текста.

  table_size = len(key1) * len(key2)
  table = [['' for _ in range(table_size)] for _ in range(table_size)]

  # Заполняем таблицу текстом по маршруту первого ключа.

  index = 0
  for i in range(table_size):
    for j in range(len(key1)):
      if i % len(key1) == j:
        table[j][i // len(key1)] = ciphertext[index]
        index += 1

  # Переставляем столбцы таблицы в соответствии со вторым ключом.

  for i in range(len(key2) - 1, -1, -1):
    for j in range(len(key2)):
      temp = table[i][j]
      table[i][j] = table[j][i]
      table[j][i] = temp

  # Заполняем выходную строку текстом по маршруту второго ключа.

  output = ''
  for i in range(table_size):
    for j in range(len(key2)):
      output += table[j][i]

  return output


def main():
  # Зашифрованное сообщение.

  ciphertext = "И_ЛБКЧУОПЧТУ_ОЬР"

  # Ключи.

  key1 = "ЛЕТО"
  key2 = "4213"

  # Расшифровываем сообщение.

  plaintext = double_transposition_decrypt(ciphertext, key1, key2)

  # Выводим расшифрованное сообщение.

  print(plaintext)


if __name__ == "__main__":
  main()
