<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $id_entered_cd = $values->code_diplome;
    $id_entered_vdi = $values->vdi;

    $strReq = "INSERT INTO `diplome` (";
    $data = "(";

    $strReq .= " `code_diplome` ";
    $data .= "'$values->code_diplome'";

    $strReq .= " ,`libelle_diplome` ";
    $data .= ",'$values->libelle_diplome'";

    $strReq .= " ,`vdi` ";
    $data .= ",'$values->vdi'";

    $strReq .= " ,`libelle_vdi` ";
    $data .= ",'$values->libelle_vdi'";

    if (isset($values->annee_deb)) {
        $strReq .= " ,`annee_deb` ";
        $data .= ",'$values->annee_deb'";
    }

    if (isset($values->annee_fin)) {
        $strReq .= " ,`annee_fin` ";
        $data .= ",'$values->annee_fin'";
    }

    $strReq .= ") VALUES $data )";

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();

    if ($error[0] == '00000') {
        if ($createReq->rowCount() != 0) {
            $resultStr = "SELECT `code_diplome`, `libelle_diplome`, `vdi`, `libelle_vdi`, `annee_deb`, `annee_fin` FROM `diplome` WHERE ";
            $resultStr .= " `code_diplome`='$id_entered_cd' AND `vdi`='$id_entered_vdi'";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_diplome = $row->code_diplome;
            $obj->libelle_diplome = $row->libelle_diplome;
            $obj->vdi = $row->vdi;
            $obj->libelle_vdi = $row->libelle_vdi;
            $obj->annee_deb = $row->annee_deb;
            $obj->annee_fin = $row->annee_fin;

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