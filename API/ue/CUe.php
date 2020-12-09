<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $strReq = "INSERT INTO `ue`";
    $postObj = json_decode(utf8_encode(file_get_contents('php://input')));

    $presence = new stdClass();
    $presence->code_ue = false;
    $presence->libelle_ue = false;
    $presence->nature = false;
    $presence->ECTS = false;
    $presence->code_ue_pere = false;
    $presence->code_sem = false;

    $strReq .= " (";
    $firstValue = true;
    foreach ($postObj->values as $values) {
        if(!$presence->code_ue) {
            if (!$firstValue)
                $strReq .= ",";
            $firstValue=false; 
            $strReq .= " `code_ue` ";
        }
        $presence->code_ue = true;

        if(!$presence->libelle_ue) {
            if (!$firstValue)
                $strReq .= ",";
            $firstValue=false;
            $strReq .= " `libelle_ue` ";
        }
        $presence->libelle_ue = true;

        if (isset($values->nature)) {
            if(!$presence->nature) {
                if (!$firstValue)
                    $strReq .= ",";
                $firstValue=false;
                $strReq .= " `nature` ";
            }
            $presence->nature = true;
        }

        if (isset($values->ECTS)) {
            if(!$presence->ECTS) {
                if (!$firstValue)
                    $strReq .= ",";
                $firstValue=false;
                $strReq .= " `ECTS` ";
            }
            $presence->ECTS = true;
        }

        if (isset($values->code_ue_pere)) {
            if(!$presence->code_ue_pere) {
                if (!$firstValue)
                    $strReq .= ",";
                $firstValue=false;
                $strReq .= " `code_ue_pere` ";
            }
            $presence->code_ue_pere = true;
        }

        if (isset($values->code_sem)) {
            if(!$presence->code_sem) {
                if (!$firstValue)
                    $strReq .= ",";
                $firstValue=false;
                $strReq .= " `code_sem` ";
            }
            $presence->code_sem = true;
        }
    }
    $strReq .= ") VALUES ";

    $firstValue = true;
    foreach ($postObj->values as $values) { //Une fois qu'on a déterminé les clés présentes on rajoute le VALUES à la requête
        if (!$firstValue)
            $data = ",(";
        else 
            $data = "(";
        $firstValue=false;

        $data .= "`$values->code_ue`";
        $data .= ",`$values->libelle_ue`";
        
        if ($presence->nature) {
            if(isset($values->nature))
                $data .= ",`$values->nature`";
            else 
                $data .= ",`U`";
        }

        if ($presence->ECTS) {
            if(isset($values->ECTS))
                $data .= ",`$values->ECTS`";
            else 
                $data .= ",`2700`";
        }

        if ($presence->code_ue_pere) {
            if(isset($values->code_ue_pere))
                $data .= ",`$values->code_ue_pere`";
            else 
                $data .= ",`NULL`";
        }

        if ($presence->code_sem) {
            if(isset($values->code_sem))
                $data .= ",`$values->code_sem`";
            else 
                $data .= ",`NULL`";
        }

        $data .= ")";
        $strReq .= $data;
    }


    $requete = $db->query($strReq);

    $ue = new stdClass();
    $ue->success = true;

    echo json_encode($ue);
} else {
    echo json_encode($connectionDB);
}
