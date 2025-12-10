IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Analysis] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_Analysis] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [NameComplete] nvarchar(200) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [Clinics] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Address] nvarchar(300) NOT NULL,
    [PhoneNumber] nvarchar(10) NOT NULL,
    CONSTRAINT [PK_Clinics] PRIMARY KEY ([Id])
);

CREATE TABLE [Insurances] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Insurances] PRIMARY KEY ([Id])
);

CREATE TABLE [Medicines] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(200) NOT NULL,
    [Format] nvarchar(50) NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Medicines] PRIMARY KEY ([Id])
);

CREATE TABLE [Permissions] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(500) NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
);

CREATE TABLE [Reasons] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NULL,
    [Available] bit NOT NULL,
    CONSTRAINT [PK_Reasons] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Doctors] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [Specialty] nvarchar(200) NOT NULL,
    [AboutMe] nvarchar(500) NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Doctors_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [DaysAvailable] (
    [Id] int NOT NULL IDENTITY,
    [ClinicId] int NOT NULL,
    [StartTime] time NOT NULL,
    [EndTime] time NOT NULL,
    [Date] date NOT NULL,
    [Limit] int NOT NULL,
    CONSTRAINT [PK_DaysAvailable] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DaysAvailable_Clinics_ClinicId] FOREIGN KEY ([ClinicId]) REFERENCES [Clinics] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Patients] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [InsuranceId] int NOT NULL,
    [Identification] nvarchar(max) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Bloodtype] int NOT NULL,
    [Gender] int NOT NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Patients_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Patients_Insurances_InsuranceId] FOREIGN KEY ([InsuranceId]) REFERENCES [Insurances] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Consultations] (
    [Id] int NOT NULL IDENTITY,
    [PatientId] int NOT NULL,
    [ReasonId] int NOT NULL,
    [DayAvailableId] int NOT NULL,
    [State] int NOT NULL,
    [Turn] int NOT NULL,
    CONSTRAINT [PK_Consultations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Consultations_DaysAvailable_DayAvailableId] FOREIGN KEY ([DayAvailableId]) REFERENCES [DaysAvailable] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Consultations_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Consultations_Reasons_ReasonId] FOREIGN KEY ([ReasonId]) REFERENCES [Reasons] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [HistoryMedicaments] (
    [PatientId] int NOT NULL,
    [MedicineId] int NOT NULL,
    [Id] int NOT NULL,
    [StartMedication] date NOT NULL,
    [EndMedication] date NULL,
    CONSTRAINT [PK_HistoryMedicaments] PRIMARY KEY ([PatientId], [MedicineId]),
    CONSTRAINT [FK_HistoryMedicaments_Medicines_MedicineId] FOREIGN KEY ([MedicineId]) REFERENCES [Medicines] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_HistoryMedicaments_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [MedicalDocuments] (
    [Id] int NOT NULL IDENTITY,
    [PatientId] int NOT NULL,
    [FileName] nvarchar(max) NOT NULL,
    [FileUrl] nvarchar(max) NOT NULL,
    [DocumentType] nvarchar(25) NOT NULL,
    CONSTRAINT [PK_MedicalDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MedicalDocuments_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [NotesPatients] (
    [Id] int NOT NULL IDENTITY,
    [PatientId] int NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [Content] nvarchar(2000) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdateAt] datetime2 NOT NULL,
    CONSTRAINT [PK_NotesPatients] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NotesPatients_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [NotesConsultations] (
    [Id] int NOT NULL IDENTITY,
    [ConsultationId] int NOT NULL,
    [Title] nvarchar(200) NOT NULL,
    [Content] nvarchar(2000) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdateAt] datetime2 NOT NULL,
    CONSTRAINT [PK_NotesConsultations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NotesConsultations_Consultations_ConsultationId] FOREIGN KEY ([ConsultationId]) REFERENCES [Consultations] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Prescriptions] (
    [Id] int NOT NULL IDENTITY,
    [ConsultationId] int NOT NULL,
    [GeneralRecomendations] nvarchar(1000) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastPrint] datetime2 NOT NULL,
    CONSTRAINT [PK_Prescriptions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Prescriptions_Consultations_ConsultationId] FOREIGN KEY ([ConsultationId]) REFERENCES [Consultations] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [PrescriptionsAnalysis] (
    [PrescriptionId] int NOT NULL,
    [AnalysisId] int NOT NULL,
    [Recomendations] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_PrescriptionsAnalysis] PRIMARY KEY ([PrescriptionId], [AnalysisId]),
    CONSTRAINT [FK_PrescriptionsAnalysis_Analysis_AnalysisId] FOREIGN KEY ([AnalysisId]) REFERENCES [Analysis] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PrescriptionsAnalysis_Prescriptions_PrescriptionId] FOREIGN KEY ([PrescriptionId]) REFERENCES [Prescriptions] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [PrescriptionsMedicines] (
    [PrescriptionId] int NOT NULL,
    [MedicineId] int NOT NULL,
    [StartDosage] date NOT NULL,
    [EndDosage] date NOT NULL,
    [Instructions] nvarchar(300) NOT NULL,
    CONSTRAINT [PK_PrescriptionsMedicines] PRIMARY KEY ([PrescriptionId], [MedicineId]),
    CONSTRAINT [FK_PrescriptionsMedicines_Medicines_MedicineId] FOREIGN KEY ([MedicineId]) REFERENCES [Medicines] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PrescriptionsMedicines_Prescriptions_PrescriptionId] FOREIGN KEY ([PrescriptionId]) REFERENCES [Prescriptions] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [PrescriptionsPermisions] (
    [PrescriptionId] int NOT NULL,
    [PermissionId] int NOT NULL,
    CONSTRAINT [PK_PrescriptionsPermisions] PRIMARY KEY ([PrescriptionId], [PermissionId]),
    CONSTRAINT [FK_PrescriptionsPermisions_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PrescriptionsPermisions_Prescriptions_PrescriptionId] FOREIGN KEY ([PrescriptionId]) REFERENCES [Prescriptions] ([Id]) ON DELETE NO ACTION
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[Analysis]'))
    SET IDENTITY_INSERT [Analysis] ON;
INSERT INTO [Analysis] ([Id], [Description], [Name])
VALUES (1, N'Prueba para ver tus niveles de globulos rojos y blancos ademas de otros datos, no duele.', N'Prueba de sangre'),
(2, N'Un estandar para ver infecciones.', N'Analisis de orina'),
(3, N'Imagen para ver fracturas o anomalias a nivel oseo.', N'Radiografia');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[Analysis]'))
    SET IDENTITY_INSERT [Analysis] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES (N'admin-role-id', N'admin-role-stamp-001', N'Admin', N'ADMIN'),
(N'doctor-role-id', N'doctor-role-stamp-002', N'Doctor', N'DOCTOR'),
(N'user-role-id', N'user-role-stamp-003', N'User', N'USER');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NameComplete', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] ON;
INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NameComplete], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'12e6b0e1-1eb6-4a4b-8b57-a7d8adf78sdf', 0, N'rufino-concurrency-004', N'rufino@mediagenda.com', CAST(1 AS bit), CAST(0 AS bit), NULL, N'Rufino Alcachofa', N'RUFINO@MEDIAGENDA.COM', NULL, N'AQAAAAIAAYagAAAAEMYtGJF/SM5CogXcBNxMt7wjuA8/1by854I67fOXznLB9REB7YSpdV6xE4RIs0Q8Eg==', N'8491782495', CAST(0 AS bit), N'RUFINO-SECURITY-STAMP-004', CAST(0 AS bit), NULL),
(N'12e6b0e1-1eb6-4a4b-8b57-d1a00b31cb46', 0, N'alva-concurrency-003', N'alva@mediagenda.com', CAST(1 AS bit), CAST(0 AS bit), NULL, N'Alva Alcachofa', N'ALVA@MEDIAGENDA.COM', NULL, N'AQAAAAIAAYagAAAAEMYtGJF/SM5CogXcBNxMt7wjuA8/1by854I67fOXznLB9REB7YSpdV6xE4RIs0Q8Eg==', N'8093543337', CAST(0 AS bit), N'ALVA-SECURITY-STAMP-003', CAST(0 AS bit), NULL),
(N'1ebe636e-b277-47ea-a2f8-6502cd988476', 0, N'pedro-concurrency-002', N'pedro@mediagenda.com', CAST(1 AS bit), CAST(0 AS bit), NULL, N'Pedro Alcachofa', N'PEDRO@MEDIAGENDA.COM', NULL, N'AQAAAAIAAYagAAAAEMYtGJF/SM5CogXcBNxMt7wjuA8/1by854I67fOXznLB9REB7YSpdV6xE4RIs0Q8Eg==', N'8093454567', CAST(0 AS bit), N'PEDRO-SECURITY-STAMP-002', CAST(0 AS bit), NULL),
(N'7bd25c44-f324-41f7-aae9-43a2f744ef46', 0, N'doctor-concurrency-001', N'dr.martinez@mediagenda.com', CAST(1 AS bit), CAST(0 AS bit), NULL, N'Carlos Martínez Pérez', N'DR.MARTINEZ@MEDIAGENDA.COM', NULL, N'AQAAAAIAAYagAAAAEMYtGJF/SM5CogXcBNxMt7wjuA8/1by854I67fOXznLB9REB7YSpdV6xE4RIs0Q8Eg==', N'8091234567', CAST(0 AS bit), N'DOCTOR-SECURITY-STAMP-001', CAST(0 AS bit), NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NameComplete', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Name', N'PhoneNumber') AND [object_id] = OBJECT_ID(N'[Clinics]'))
    SET IDENTITY_INSERT [Clinics] ON;
INSERT INTO [Clinics] ([Id], [Address], [Name], [PhoneNumber])
VALUES (1, N'KM 9 de la Autopista Duarte', N'Clinica 9', N'8095475432');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Name', N'PhoneNumber') AND [object_id] = OBJECT_ID(N'[Clinics]'))
    SET IDENTITY_INSERT [Clinics] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Insurances]'))
    SET IDENTITY_INSERT [Insurances] ON;
INSERT INTO [Insurances] ([Id], [Name])
VALUES (1, N'Humano');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Insurances]'))
    SET IDENTITY_INSERT [Insurances] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Format', N'IsActive', N'Name') AND [object_id] = OBJECT_ID(N'[Medicines]'))
    SET IDENTITY_INSERT [Medicines] ON;
INSERT INTO [Medicines] ([Id], [Description], [Format], [IsActive], [Name])
VALUES (1, N'An analgesic and antipyretic medication used to treat pain and fever.', N'Tablet', CAST(1 AS bit), N'Paracetamol'),
(2, N'A nonsteroidal anti-inflammatory drug (NSAID) used to reduce inflammation, pain, and fever.', N'Capsule', CAST(1 AS bit), N'Ibuprofen'),
(3, N'A penicillin-type antibiotic used to treat a variety of bacterial infections.', N'Tablet', CAST(1 AS bit), N'Amoxicillin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Format', N'IsActive', N'Name') AND [object_id] = OBJECT_ID(N'[Medicines]'))
    SET IDENTITY_INSERT [Medicines] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Available', N'Description', N'Title') AND [object_id] = OBJECT_ID(N'[Reasons]'))
    SET IDENTITY_INSERT [Reasons] ON;
INSERT INTO [Reasons] ([Id], [Available], [Description], [Title])
VALUES (1, CAST(1 AS bit), NULL, N'Consulta'),
(2, CAST(1 AS bit), NULL, N'Primera vez'),
(3, CAST(1 AS bit), NULL, N'Permiso'),
(4, CAST(1 AS bit), NULL, N'Referimiento'),
(5, CAST(1 AS bit), NULL, N'Entrega de Estudios');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Available', N'Description', N'Title') AND [object_id] = OBJECT_ID(N'[Reasons]'))
    SET IDENTITY_INSERT [Reasons] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] ON;
INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
VALUES (N'user-role-id', N'12e6b0e1-1eb6-4a4b-8b57-a7d8adf78sdf'),
(N'user-role-id', N'12e6b0e1-1eb6-4a4b-8b57-d1a00b31cb46'),
(N'user-role-id', N'1ebe636e-b277-47ea-a2f8-6502cd988476'),
(N'doctor-role-id', N'7bd25c44-f324-41f7-aae9-43a2f744ef46');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClinicId', N'Date', N'EndTime', N'Limit', N'StartTime') AND [object_id] = OBJECT_ID(N'[DaysAvailable]'))
    SET IDENTITY_INSERT [DaysAvailable] ON;
INSERT INTO [DaysAvailable] ([Id], [ClinicId], [Date], [EndTime], [Limit], [StartTime])
VALUES (1, 1, '2025-10-21', '16:00:00', 15, '08:00:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClinicId', N'Date', N'EndTime', N'Limit', N'StartTime') AND [object_id] = OBJECT_ID(N'[DaysAvailable]'))
    SET IDENTITY_INSERT [DaysAvailable] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AboutMe', N'Specialty', N'UserId') AND [object_id] = OBJECT_ID(N'[Doctors]'))
    SET IDENTITY_INSERT [Doctors] ON;
INSERT INTO [Doctors] ([Id], [AboutMe], [Specialty], [UserId])
VALUES (1, N'Especialista en cardiologia con 10 años de experiencia.', N'Cardiologia', N'7bd25c44-f324-41f7-aae9-43a2f744ef46');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AboutMe', N'Specialty', N'UserId') AND [object_id] = OBJECT_ID(N'[Doctors]'))
    SET IDENTITY_INSERT [Doctors] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bloodtype', N'DateOfBirth', N'Gender', N'Identification', N'InsuranceId', N'UserId') AND [object_id] = OBJECT_ID(N'[Patients]'))
    SET IDENTITY_INSERT [Patients] ON;
INSERT INTO [Patients] ([Id], [Bloodtype], [DateOfBirth], [Gender], [Identification], [InsuranceId], [UserId])
VALUES (1, 6, '2004-01-25T00:00:00.0000000', 0, N'40214353345', 1, N'1ebe636e-b277-47ea-a2f8-6502cd988476'),
(2, 7, '2004-05-30T00:00:00.0000000', 1, N'40243453345', 1, N'12e6b0e1-1eb6-4a4b-8b57-d1a00b31cb46'),
(3, 0, '2000-03-06T00:00:00.0000000', 0, N'40234533343', 1, N'12e6b0e1-1eb6-4a4b-8b57-a7d8adf78sdf');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bloodtype', N'DateOfBirth', N'Gender', N'Identification', N'InsuranceId', N'UserId') AND [object_id] = OBJECT_ID(N'[Patients]'))
    SET IDENTITY_INSERT [Patients] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DayAvailableId', N'PatientId', N'ReasonId', N'State', N'Turn') AND [object_id] = OBJECT_ID(N'[Consultations]'))
    SET IDENTITY_INSERT [Consultations] ON;
INSERT INTO [Consultations] ([Id], [DayAvailableId], [PatientId], [ReasonId], [State], [Turn])
VALUES (1, 1, 1, 1, 0, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DayAvailableId', N'PatientId', N'ReasonId', N'State', N'Turn') AND [object_id] = OBJECT_ID(N'[Consultations]'))
    SET IDENTITY_INSERT [Consultations] OFF;

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE INDEX [IX_Consultations_DayAvailableId] ON [Consultations] ([DayAvailableId]);

CREATE INDEX [IX_Consultations_PatientId] ON [Consultations] ([PatientId]);

CREATE INDEX [IX_Consultations_ReasonId] ON [Consultations] ([ReasonId]);

CREATE INDEX [IX_DaysAvailable_ClinicId] ON [DaysAvailable] ([ClinicId]);

CREATE UNIQUE INDEX [IX_Doctors_UserId] ON [Doctors] ([UserId]);

CREATE INDEX [IX_HistoryMedicaments_MedicineId] ON [HistoryMedicaments] ([MedicineId]);

CREATE INDEX [IX_MedicalDocuments_PatientId] ON [MedicalDocuments] ([PatientId]);

CREATE INDEX [IX_NotesConsultations_ConsultationId] ON [NotesConsultations] ([ConsultationId]);

CREATE INDEX [IX_NotesPatients_PatientId] ON [NotesPatients] ([PatientId]);

CREATE INDEX [IX_Patients_InsuranceId] ON [Patients] ([InsuranceId]);

CREATE UNIQUE INDEX [IX_Patients_UserId] ON [Patients] ([UserId]);

CREATE UNIQUE INDEX [IX_Prescriptions_ConsultationId] ON [Prescriptions] ([ConsultationId]);

CREATE INDEX [IX_PrescriptionsAnalysis_AnalysisId] ON [PrescriptionsAnalysis] ([AnalysisId]);

CREATE INDEX [IX_PrescriptionsMedicines_MedicineId] ON [PrescriptionsMedicines] ([MedicineId]);

CREATE INDEX [IX_PrescriptionsPermisions_PermissionId] ON [PrescriptionsPermisions] ([PermissionId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251210005433_MigracionFinalConTodo', N'9.0.10');

COMMIT;
GO

