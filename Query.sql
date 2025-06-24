


 
-- admin table
CREATE TABLE admin (
    id VARCHAR(20) NOT NULL,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    image VARCHAR(100) NOT NULL,
    profession VARCHAR(20) NOT NULL,
    updationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id)
);

-- admin_ticket table
CREATE TABLE admin_ticket (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL
);

-- contact table
CREATE TABLE contact (
    name VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    number INT NOT NULL,
    message VARCHAR(1000) NOT NULL
);

-- prequest table
CREATE TABLE prequest (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NULL,
    email VARCHAR(255) NULL,
    contactno VARCHAR(11) NULL,
    company VARCHAR(255) NULL,
    services TEXT NULL,
    others VARCHAR(255) NULL,
    query NVARCHAR(MAX) NULL,
    status BIT DEFAULT 0,
    posting_date DATE NULL,
    remark NVARCHAR(MAX) NULL
);


-- ticket table
CREATE TABLE ticket (
    id INT IDENTITY(1,1) PRIMARY KEY,
    ticket_id VARCHAR(11) NULL,
    email_id VARCHAR(300) NULL,
    subject VARCHAR(300) NULL,
    task_type VARCHAR(300) NULL,
    prioprity VARCHAR(300) NULL,
    ticket NVARCHAR(MAX) NULL,
    attachment VARCHAR(300) NULL,
    status VARCHAR(300) NULL,
    admin_remark NVARCHAR(MAX) NULL,
    posting_date DATE NULL,
    admin_remark_date DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- tutors table
CREATE TABLE tutors (
    id VARCHAR(20) NOT NULL,
    name VARCHAR(50) NOT NULL,
    profession VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    image VARCHAR(100) NOT NULL,
    updationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id)
);


-- user_ticket table
CREATE TABLE user_ticket (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NULL,
    email VARCHAR(255) NULL,
    alt_email VARCHAR(255) NULL,
    password VARCHAR(255) NULL,
    mobile VARCHAR(255) NULL,
    gender VARCHAR(255) NULL,
    address VARCHAR(500) NULL,
    status INT NULL,
    posting_date DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- users table
CREATE TABLE users (
    id VARCHAR(20) NOT NULL,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    password VARCHAR(50) DEFAULT '40bd001563085fc35165329ea1ff5c5ecbdbbeef',
    image VARCHAR(100) NOT NULL,
    genero VARCHAR(255) NOT NULL,
    telefono VARCHAR(255) NOT NULL,
    fecha_asig DATETIME DEFAULT CURRENT_TIMESTAMP,
    etapa_educativa VARCHAR(255) NOT NULL,
    grado VARCHAR(255) NULL,
    updationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id)
);


-- videogame table
CREATE TABLE videogame (
    id VARCHAR(400) NOT NULL,
    name VARCHAR(400) NOT NULL,
    carpeta VARCHAR(1000) NOT NULL,
    type VARCHAR(15) NOT NULL,
    imagen VARCHAR(100) NOT NULL,
    tutor_id VARCHAR(20) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (tutor_id) REFERENCES tutors(id)
);


-- usercheck table
CREATE TABLE usercheck (
    id INT IDENTITY(1,1) PRIMARY KEY,
    logindate VARCHAR(255) NULL,
    logintime VARCHAR(255) NULL,
    user_id VARCHAR(20) NULL,
    username VARCHAR(255) NULL,
    email VARCHAR(255) NULL,
    ip VARBINARY(16) NULL,
    mac VARBINARY(16) NULL,
    city VARCHAR(255) NULL,
    country VARCHAR(255) NULL,
    FOREIGN KEY (user_id) REFERENCES users(id)
);



-- playlist table
CREATE TABLE playlist (
    id VARCHAR(20) NOT NULL,
    tutor_id VARCHAR(20) NOT NULL,
    title VARCHAR(100) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    thumb VARCHAR(100) NOT NULL,
    date DATETIME DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(20) DEFAULT 'deactive',
    categoria VARCHAR(255) DEFAULT 'nn',
    iframe VARCHAR(1000) NULL,
    UpdationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    is_diplomado BIT DEFAULT 0,
    is_visto BIT DEFAULT 0,
    PRIMARY KEY (id),
    FOREIGN KEY (tutor_id) REFERENCES tutors(id)
);


-- ovas table
CREATE TABLE ovas (
    id VARCHAR(25) NOT NULL,
    playlist_id VARCHAR(20) NOT NULL,
    name VARCHAR(255) NOT NULL,
    iframe VARCHAR(255) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (playlist_id) REFERENCES playlist(id)
);

-- download table
CREATE TABLE download (
    id VARCHAR(255) NOT NULL,
    user_id VARCHAR(20) NOT NULL,
    playlist_id VARCHAR(20) NOT NULL,
    date DATETIME NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (playlist_id) REFERENCES playlist(id)
);


-- content table
CREATE TABLE content (
    id VARCHAR(85) NOT NULL,
    tutor_id VARCHAR(20) NOT NULL,
    playlist_id VARCHAR(20) NOT NULL,
    title VARCHAR(100) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    video VARCHAR(100) NOT NULL,
    thumb VARCHAR(100) NOT NULL,
    date DATETIME DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(20) DEFAULT 'deactive',
    type VARCHAR(15) DEFAULT 'Video',
    PRIMARY KEY (id),
    FOREIGN KEY (tutor_id) REFERENCES tutors(id),
    FOREIGN KEY (playlist_id) REFERENCES playlist(id)
);


-- avance table
CREATE TABLE avance (
    playlist_id VARCHAR(20) NOT NULL,
    type VARCHAR(400) NOT NULL,
    content_id VARCHAR(85) NOT NULL,
    user_id VARCHAR(20) NOT NULL,
    FOREIGN KEY (playlist_id) REFERENCES playlist(id),
    FOREIGN KEY (content_id) REFERENCES content(id),
    FOREIGN KEY (user_id) REFERENCES users(id)
);



-- bookmark table
CREATE TABLE bookmark (
    user_id VARCHAR(20) NULL,
    playlist_id VARCHAR(20) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (playlist_id) REFERENCES playlist(id)
);

-- certificates table
CREATE TABLE certificates (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id VARCHAR(20) NULL,
    playlist_id VARCHAR(20) NULL,
    certificate_path VARCHAR(255) NOT NULL,
    created_at DATETIME NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (playlist_id) REFERENCES playlist(id)
);


-- comments table
CREATE TABLE comments (
    id VARCHAR(20) NOT NULL,
    content_id VARCHAR(85) NULL,
    user_id VARCHAR(20) NOT NULL,
    tutor_id VARCHAR(20) NOT NULL,
    comment VARCHAR(1000) NOT NULL,
    date DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    FOREIGN KEY (content_id) REFERENCES content(id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (tutor_id) REFERENCES tutors(id)
);







-- eventoscalendar table
CREATE TABLE eventoscalendar (
    id INT IDENTITY(1,1) PRIMARY KEY,
    tutor_id VARCHAR(20) NULL,
    user_id VARCHAR(20) NULL,
    playlist_id VARCHAR(20) NULL,
    evento VARCHAR(250) NOT NULL,
    color_evento VARCHAR(20) NOT NULL,
    fecha_inicio VARCHAR(20) NOT NULL,
    fecha_fin VARCHAR(20) NOT NULL,
    estado BIT NOT NULL,
    FOREIGN KEY (tutor_id) REFERENCES tutors(id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (playlist_id) REFERENCES playlist(id)
);


-- flipbook table
CREATE TABLE flipbook (
    id VARCHAR(255) NOT NULL,
    playlist_id VARCHAR(20) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    pages INT NOT NULL,
    directory VARCHAR(1000) NOT NULL,
    archivo VARCHAR(500) NOT NULL,
    status VARCHAR(50) NOT NULL,
    type VARCHAR(15) DEFAULT 'Libro',
    PRIMARY KEY (id),
    FOREIGN KEY (playlist_id) REFERENCES playlist(id)
);




-- likes table
CREATE TABLE likes (
    user_id VARCHAR(20) NOT NULL,
    tutor_id VARCHAR(20) NOT NULL,
    content_id VARCHAR(85) NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (tutor_id) REFERENCES tutors(id),
    FOREIGN KEY (content_id) REFERENCES content(id)
);


-- tblresult table
CREATE TABLE tblresult (
    id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId VARCHAR(20) NOT NULL,
    ClassId VARCHAR(50) NOT NULL,
    subjects_id VARCHAR(100) NOT NULL,
    marks VARCHAR(50) NOT NULL,
    PostingDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (StudentId) REFERENCES users(id)
);

-- tblsubjects table
CREATE TABLE tblsubjects (
    id VARCHAR(50) NOT NULL,
    playlist_id VARCHAR(20) NOT NULL,
    SubjectName VARCHAR(100) NOT NULL,
    Creationdate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdationDate DATETIME NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (playlist_id) REFERENCES playlist(id)
);



