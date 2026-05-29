-- AI Generated based on the entities created
-- 1. Insert Account Holders and capture their generated IDs
DECLARE @AliceId INT, @BobId INT, @CharlieId INT;

INSERT INTO [AccountHolders] ([Name]) VALUES ('Alice Smith');
SET @AliceId = SCOPE_IDENTITY();

INSERT INTO [AccountHolders] ([Name]) VALUES ('Bob Jones');
SET @BobId = SCOPE_IDENTITY();

INSERT INTO [AccountHolders] ([Name]) VALUES ('Charlie Brown');
SET @CharlieId = SCOPE_IDENTITY();


-- 2. Insert Loans matching the Status integers (e.g., 0 = Active, 1 = Inactive, 2 = Paid)
INSERT INTO [Loans] ([AmountRequested], [AmountPaid], [Status], [AccountHolderId])
VALUES
    -- Alice has an Active loan and a Paid loan
    (5000.0000, 1200.0000, 0, @AliceId),
    (1500.0000, 1500.0000, 2, @AliceId),

    -- Bob has an Inactive loan
    (10000.0000, 0.0000, 1, @BobId),
    
    -- Charlie has an Active loan
    (2500.0000, 2500.0000, 2, @CharlieId);
