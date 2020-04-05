CREATE TABLE Events
(
    user_id SERIAL NOT NULL,
    event_id SERIAL NOT NULL,
    entered BOOLEAN NOT NULL,
    happened_at TIMESTAMP NOT NULL,
    
    Primary key(user_id, event_id)
);

CREATE TABLE Users
(
    user_id SERIAL NOT NUll PRIMARY KEY,
    user_name VARCHAR(30) NOT NULL,
    subscription_expires_at timestamp NOT NULL
    
);

CREATE TABLE MaxVals (
    user_id SERIAL NOT NUll PRIMARY KEY,
    last_event_id INT NOT NULL  
);