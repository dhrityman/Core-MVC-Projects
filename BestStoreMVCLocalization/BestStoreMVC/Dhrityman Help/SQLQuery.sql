Select * from Products
---------------------------

INSERT INTO Products (name,Brand,Categorye,Price,Description,ImageFileName,CreatedAt) 
VALUES
('Wireless Mouse', 'Logitech', 'Electronics', 29.99, 'A high-precision wireless mouse with ergonomic design.', 'wireless_mouse.jpg', SYSDATETIME()),
('Wireless Mouse', 'Dell', 'Electronics', 30.99, 'A high-precision wireless mouse with ergonomic design.', 'wireless_mouse.jpg', SYSDATETIME()),
('Wireless Mouse', 'LG', 'Electronics', 31.99, 'A high-precision wireless mouse with ergonomic design.', 'wireless_mouse.jpg', SYSDATETIME()),

('Bluetooth Headphones', 'Sony', 'Electronics', 99.99, 'Noise-cancelling over-ear headphones with long battery life.', 'bluetooth_headphones.jpg', SYSDATETIME()),
('Bluetooth Headphones', 'Samsung', 'Electronics', 100.99, 'Noise-cancelling over-ear headphones with long battery life.', 'bluetooth_headphones.jpg', SYSDATETIME()),
('Bluetooth Headphones', 'Apple', 'Electronics', 101.99, 'Noise-cancelling over-ear headphones with long battery life.', 'bluetooth_headphones.jpg', SYSDATETIME()),

('Smartphone', 'Apple', 'Electronics', 999.99, 'Latest model with advanced features and high-resolution camera.', 'smartphone.jpg', SYSDATETIME()),
('Smartphone', 'Samsung', 'Electronics', 1000.99, 'Latest model with advanced features and high-resolution camera.', 'smartphone.jpg', SYSDATETIME()),
('Smartphone', 'Nokia', 'Electronics', 899.99, 'Latest model with advanced features and high-resolution camera.', 'smartphone.jpg', SYSDATETIME()),

('Gaming Laptop', 'Dell', 'Computers', 1299.99, 'High-performance laptop designed for gaming and multimedia.', 'gaming_laptop.jpg', SYSDATETIME()),
('Gaming Laptop', 'Lenavo', 'Computers', 1399.99, 'High-performance laptop designed for gaming and multimedia.', 'gaming_laptop.jpg', SYSDATETIME()),
('Gaming Laptop', 'HP', 'Computers', 1499.99, 'High-performance laptop designed for gaming and multimedia.', 'gaming_laptop.jpg', SYSDATETIME()),

('4K Monitor', 'Samsung', 'Electronics', 399.99, 'Ultra HD monitor with vibrant colors and fast refresh rate.', '4k_monitor.jpg', SYSDATETIME()),
('4K Monitor', 'LG', 'Electronics', 499.99, 'Ultra HD monitor with vibrant colors and fast refresh rate.', '4k_monitor.jpg', SYSDATETIME()),
('4K Monitor', 'HP', 'Electronics', 599.99, 'Ultra HD monitor with vibrant colors and fast refresh rate.', '4k_monitor.jpg', SYSDATETIME());