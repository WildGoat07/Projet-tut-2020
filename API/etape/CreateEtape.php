<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $id_entered_code_etape = $values->code_etape;
    $id_entered_vet = $values->vet;
    
    $strReq = "INSERT INTO `etape` (";
    $data = "(";

    $strReq .= " `code_etape` ";
    $data .= "'$values->code_etape'";

    $strReq .= " ,`vet` ";
    $data .= ",'$values->vet'";

    if (isset($values->libelle_vet)) {
        $strReq .= " ,`libelle_vet` ";
        $data .= ",'$values->libelle_vet'";
    }

    if (isset($values->id_comp)) {
        $strReq .= " ,`id_comp` ";
        $data .= ",'$values->id_comp'";
    }

    if (isset($values->code_diplome)) {
        $strReq .= " ,`code_diplome` ";
        $data .= ",'$values->code_diplome'";
    }

    if (isset($values->vdi)) {
        $strReq .= " ,`vdi` ";
        $data .= ",'$values->vdi'";
    }

    $strReq .= ") VALUES $data )";

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();

    if ($error[0] == '00000') {
        if ($createReq->rowCount() != 0) {
            $resultStr = "SELECT `code_etape`, `vet`, `libelle_vet`, `id_comp`, `code_diplome`, `vdi` FROM `etape` WHERE ";
            $resultStr .= "`code_etape`='$id_entered_code_etape' AND `vet`='id_entered_vet'";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_etape = $row->code_etape;
            $obj->vet = $row->vet;
            $obj->libelle_vet = $row->libelle_vet;
            $obj->id_comp = $row->id_comp;
            $obj->code_diplome = $row->code_diplome;
            $obj->vdi = $row->vdi;
            
            $returnedValues->values[] = $obj;

        } 
        else {
            $returnedValues->success=false;
            
            $obj = new stdClass();
            $obj->error_code = '66666'; //enregistrement code d'erreur
            $obj->error_desc = '0 rows affected'; //enregistrement message d'erreru renvoyé
            $returnedValues->errors[] = $obj;
        }
    } 
    else {
        $returnedValues->success=false;

        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreur renvoyé
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);