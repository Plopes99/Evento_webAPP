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




-- Script para criar a tabela "Registrations"
CREATE TABLE Registrations (
                               registration_id SERIAL PRIMARY KEY,
                               event_id INTEGER REFERENCES Events(event_id),
                               participant_id INTEGER REFERENCES Users(user_id)
);

-- Script para inserir dados na tabela "Registrations"
INSERT INTO Registrations (event_id, participant_id)
VALUES
    (1, 1), -- Exemplo de registro para o evento com event_id = 1 e o participante com participant_id = 1
    (2, 1), -- Exemplo de registro para o evento com event_id = 2 e o participante com participant_id = 1
    (2, 2); -- Exemplo de registro para o evento com event_id = 2 e o participante com participant_id = 2




CREATE TABLE Activities (
                            activity_id SERIAL PRIMARY KEY,
                            event_id INTEGER REFERENCES Events(event_id),
                            name VARCHAR(255) NOT NULL,
                            date DATE NOT NULL,
                            time TIME NOT NULL,
                            description TEXT
);

-- Query para inserir dados na tabela "Activities"
INSERT INTO Activities (event_id, name, date, time, description)
VALUES
    (1, 'Atividade 1', '2023-06-01', '10:00:00', 'Descrição da Atividade 1'), -- Exemplo de atividade para o evento com event_id = 1
    (1, 'Atividade 2', '2023-06-02', '14:30:00', 'Descrição da Atividade 2'), -- Exemplo de atividade para o evento com event_id = 1
    (2, 'Atividade 1', '2023-06-03', '09:00:00', 'Descrição da Atividade 1'); -- Exemplo de atividade para o evento com event_id = 2


-- Script para criar a tabela "Tickets"
CREATE TABLE Tickets (
                         ticket_id SERIAL PRIMARY KEY,
                         event_id INTEGER REFERENCES Events(event_id),
                         ticket_type VARCHAR(255) NOT NULL,
                         quantity_available INTEGER NOT NULL
);

-- Query para inserir dados na tabela "Tickets"
INSERT INTO Tickets (event_id, ticket_type, quantity_available)
VALUES
    (1, 'Entrada Normal', 100), -- Exemplo de ingresso para o evento com event_id = 1
    (1, 'Entrada VIP', 50), -- Exemplo de ingresso para o evento com event_id = 1
    (2, 'Ingresso Padrão', 200), -- Exemplo de ingresso para o evento com event_id = 2
    (2, 'Ingresso Premium', 100); -- Exemplo de ingresso para o evento com event_id = 2
