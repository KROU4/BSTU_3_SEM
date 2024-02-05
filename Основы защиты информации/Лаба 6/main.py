import sympy

# Задача 1: Найти канонические разложения чисел a и b
a = 660422941
b = 36481301

canonical_a = [int(p) for p in sympy.primerange(2, a + 1) if a % p == 0]
canonical_b = [int(p) for p in sympy.primerange(2, b + 1) if b % p == 0]

print("Задача 1: Каноническое разложение a:", canonical_a)
print("Задача 1: Каноническое разложение b:", canonical_b)

# Задача 2a: Найти НОД с использованием алгоритма Евклида
def gcd_euclidean(a, b):
    while b:
        a, b = b, a % b
    return a

gcd = gcd_euclidean(a, b)
print("Задача 2a: НОД (алгоритм Евклида):", gcd)

# Задача 2б: Найти НОД, разлагая числа на простые множители
def prime_factors(n):
    factors = []
    i = 2
    while i * i <= n:
        if n % i:
            i += 1
        else:
            n //= i
            factors.append(i)
    if n > 1:
        factors.append(n)
    return factors

prime_factors_a = prime_factors(a)
prime_factors_b = prime_factors(b)

gcd = 1
for factor in prime_factors_a:
    if factor in prime_factors_b:
        gcd *= factor

print("Задача 2б: НОД (разложение на простые множители):", gcd)

# Задача 3: Найти целые u и v, удовлетворяющие соотношению Безу (au + bv = НОД)
def extended_gcd(a, b):
    if a == 0:
        return (b, 0, 1)
    else:
        g, x, y = extended_gcd(b % a, a)
        return (g, y - (b // a) * x, x)

gcd, u, v = extended_gcd(a, b)
print("Задача 3: НОД:", gcd)
print(f"Задача 3: u: {u}, v: {v}, так что {a} * {u} + {b} * {v} = {gcd}")

# Задача 4: Найти остаток от деления числа 2001^2005 на 17
result = (2001 ** 2005)
result1 = (result % 17)
print("Задача 4: Число 2001^2005:", result)
print("Задача 4: Остаток от деления 2001^2005 на 17:", result1)