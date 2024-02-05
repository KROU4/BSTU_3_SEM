USE B_MyBase;

-- Запрос 1: Перечень кодов товаров и соответствующих им наименований (сделать через иннер джоин)
SELECT product_article, detail_name
FROM Products;

-- Запрос 2: Перечень кодов поставщиков и соответствующих им наименований (Использовать соединение таблиц INNER JOIN и предикат LIKE)
SELECT supplier, supplier_code
FROM Suppliers;

-- Запрос 3: Перечень заказов с именами поставщиков и наименованиями товаров (Использовать соединение INNER JOIN, предикат BETWEEN и выражение CASE)
SELECT O.order_number, S.supplier, P.detail_name
FROM Orders O
INNER JOIN Suppliers S ON O.order_supplier = S.supplier
INNER JOIN Products P ON O.order_product_article = P.product_article;

-- Запрос 4: Полный перечень заказов (включая дату) с отсортированными оценками (Примечание: использовать соединение таблиц LEFT OUTER JOIN и функцию isnull)
SELECT O.order_number, O.order_date, O.order_detail, O.order_quantity, S.supplier, O.order_phone_number, O.order_price, P.product_article
FROM Orders O
INNER JOIN Suppliers S ON O.order_supplier = S.supplier
INNER JOIN Products P ON O.order_product_article = P.product_article
ORDER BY O.order_price DESC;

-- Запрос 5: Демонстрация коммутативности FULL OUTER JOIN с двумя таблицами (Использовать в запросах выражение IS NULL и IS NOT NULL)
-- Запрос 1 (левая таблица исключительно): (заменить нулл на ***)
SELECT P.product_article, P.detail_name
FROM Products P
LEFT OUTER JOIN Suppliers S ON P.product_article = S.supplier_code
WHERE S.supplier_code IS NULL; (ис нот)

-- Запрос 2 (правая таблица исключительно):(заменить нулл на ***)
SELECT S.supplier, S.supplier_code
FROM Products P
RIGHT OUTER JOIN Suppliers S ON P.product_article = S.supplier_code
WHERE P.product_article IS NULL; (ис нот)

-- Запрос 3 (обе таблицы): (заменить нулл на ***)
SELECT P.product_article, P.detail_name, S.supplier, S.supplier_code
FROM Products P
FULL OUTER JOIN Suppliers S ON P.product_article = S.supplier_code;


-- Запрос 6: Пересечение таблицы Products и Suppliers (CROSS JOIN)
SELECT P.product_article, P.detail_name, S.supplier, S.supplier_code
FROM Products P
CROSS JOIN Suppliers S; (чтобы результат вышел как в 1)
