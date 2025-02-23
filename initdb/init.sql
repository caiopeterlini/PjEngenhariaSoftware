
USE mydb;



CREATE TABLE client(
    client_id int NOT NULL  AUTO_INCREMENT,
    cpf  varchar(11) NOT NULL ,  
    client_name varchar(250) NOT NULL ,
    PRIMARY KEY (client_id)
);

CREATE TABLE product(
    product_id int  NOT NULL AUTO_INCREMENT,
    product_name varchar(11) NOT NULL,  
    product_price varchar(250) NOT NULL,
    product_version int ,
    PRIMARY KEY (product_id)
);


CREATE TABLE order_delivery(
    order_id int NOT NULL AUTO_INCREMENT,
    order_cpf_id varchar(11) NOT NULL,  
    order_total_price varchar(250) ,
    PRIMARY KEY (order_id),
    FOREIGN KEY (order_id) REFERENCES client (client_id) 
);


CREATE TABLE order_product(
    order_product_id int NOT NULL AUTO_INCREMENT,
    order_product_order_id int   NOT NULL,  
    order_product_product_id int NOT NULL,
    FOREIGN KEY (order_product_order_id) REFERENCES order_delivery (order_id), 
    FOREIGN KEY (order_product_product_id) REFERENCES product (product_id),
    PRIMARY KEY (order_product_id)
);