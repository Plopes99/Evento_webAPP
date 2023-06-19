/*  EVENTS     */
CREATE TABLE Events (
                        event_id SERIAL PRIMARY KEY,
                        organizer_id INT REFERENCES Users(user_id),
                        name VARCHAR(255) NOT NULL,
                        date DATE NOT NULL,
                        time TIME NOT NULL,
                        location VARCHAR(255) NOT NULL,
                        description TEXT,
                        max_capacity INT NOT NULL,
                        ticket_price DECIMAL(10, 2) NOT NULL
);

/*  USERS     */
CREATE TABLE Users (
                       user_id SERIAL PRIMARY KEY,
                       username VARCHAR(255) NOT NULL,
                       password VARCHAR(255) NOT NULL,
                       email VARCHAR(255) NOT NULL,
                       phone_number VARCHAR(255) NOT NULL,
                       role VARCHAR(20) NOT NULL
);


/*  INSERIR EVENTS     */

INSERT INTO Events (organizer_id, name, date, time, location, description, max_capacity, ticket_price)
VALUES
    (1, 'Evento 1', '2023-06-01', '10:00:00', 'Local 1', 'Descrição do Evento 1', 100, 10.99),
    (2, 'Evento 2', '2023-06-05', '15:30:00', 'Local 2', 'Descrição do Evento 2', 50, 15.99),
    (1, 'Evento 3', '2023-06-10', '18:00:00', 'Local 3', 'Descrição do Evento 3', 200, 8.99);



/*  INSERIR USERS    */

INSERT INTO Users (username, password, email, phone_number, role)
VALUES
    ('user1', 'password1', 'user1@example.com', '123456789', 'participant'),
    ('user2', 'password2', 'user2@example.com', '987654321', 'organizer'),
    ('user3', 'password3', 'user3@example.com', '555555555', 'participant');

/*   DOCKER   
     docker run --name es2-db -p 5432:5432 -e POSTGRES_PASSWORD=es2 -e 
     POSTGRES_USER=es2 -e POSTGRES_DB=es2 -d postgres
*/