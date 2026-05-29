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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260528175235_Initial'
)
BEGIN
    CREATE TABLE [AccountHolders] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_AccountHolders] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260528175235_Initial'
)
BEGIN
    CREATE TABLE [Loans] (
        [Id] int NOT NULL IDENTITY,
        [AmountRequested] decimal(19,4) NOT NULL,
        [AmountPaid] decimal(19,4) NOT NULL,
        [Status] int NOT NULL,
        [AccountHolderId] int NOT NULL,
        CONSTRAINT [PK_Loans] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Loans_AccountHolders_AccountHolderId] FOREIGN KEY ([AccountHolderId]) REFERENCES [AccountHolders] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260528175235_Initial'
)
BEGIN
    CREATE INDEX [IX_Loans_AccountHolderId] ON [Loans] ([AccountHolderId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260528175235_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260528175235_Initial', N'10.0.8');
END;

COMMIT;
GO

