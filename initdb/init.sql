
USE mydb;



CREATE TABLE client (
    cli_id int NOT NULL AUTO_INCREMENT,
    cli_cpf varchar(11) NOT NULL,
    cli_name varchar(250) NOT NULL,
    PRIMARY KEY (cli_id)
);

CREATE TABLE product (
    id int NOT NULL AUTO_INCREMENT,
    p_name varchar(250) NOT NULL,
    price decimal(10,2) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE orders (
    id int NOT NULL AUTO_INCREMENT,
    cpf_id int NOT NULL,
    total_price decimal(10,2) NOT NULL,
    FOREIGN KEY (cpf_id) REFERENCES client (cli_id),
    PRIMARY KEY (id)
);

CREATE TABLE item_order (
    id int NOT NULL AUTO_INCREMENT,
    order_id int NOT NULL,
    product_id int NOT NULL,
    quantity int NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders (id),
    FOREIGN KEY (product_id) REFERENCES product (id),
    PRIMARY KEY (id)
);