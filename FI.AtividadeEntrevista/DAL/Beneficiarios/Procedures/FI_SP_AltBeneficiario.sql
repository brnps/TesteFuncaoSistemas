﻿CREATE PROC FI_SP_AltBeneficiario
    @NOME          VARCHAR (50) ,
	@CPF		   VARCHAR (14),    
	@ID            BIGINT
AS
BEGIN
	UPDATE BENEFICIARIOS 
	SET 
		NOME = @NOME, 
		CPF = @CPF		
	WHERE ID = @ID
END