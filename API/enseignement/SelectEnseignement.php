<?php
require_once '../app/Database.php';
require_once '../utilities.php';

header('Content-Type: application/json');

$enseignement = new stdClass();
$enseignement->values = [];
$enseignement->success = true;
$enseignement->errors = [];

$strReq = "SELECT `code_ec`, `annee`, `eff_prev`, `eff_reel`, `GpCM`, `GpEI`, `GpTD`, `GpTP`, `GpTPL`, `GpPRJ`
, `GpCMSer`, `GpEISer`, `GpTDSer`, `GpTPSer`, `GpTPLSer`, `GpPRJSer` FROM `enseignement`";
$postObj = json_decode(file_get_contents('php://input'));

$whereSet = false;

if (isset($postObj->filters)) {
    $firstFilter = true;

    if (isset($postObj->filters->code_ec)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->code_ec as $code_ec) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`code_ec` = \"$code_ec\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->annee)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->annee as $annee) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`annee` = \"$annee\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->eff_prev)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        if( $eff_prev == null )
            $strReq .= "`eff_prev` IS NULL";
        else {
            $minSet = false;
            if (isset($postObj->filters->eff_prev->min)) {
                $strReq .= "`eff_prev` >= " . $postObj->filters->eff_prev->min;
                $minSet = true;
            }
            if (isset($postObj->filters->eff_prev->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`eff_prev` <= " . $postObj->filters->eff_prev->max;
            }
        }
    }
    if (isset($postObj->filters->eff_reel)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        if( $eff_reel == null )
            $strReq .= "`eff_reel` IS NULL";
        else {
            $minSet = false;
            if (isset($postObj->filters->eff_reel->min)) {
                $strReq .= "`eff_reel` >= " . $postObj->filters->eff_reel->min;
                $minSet = true;
            }
            if (isset($postObj->filters->eff_reel->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`eff_reel` <= " . $postObj->filters->eff_reel->max;
            }
        }
    }
    if (isset($postObj->filters->GpCM)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpCM->min)) {
            $strReq .= "`GpCM` >= " . $postObj->filters->GpCM->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpCM->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpCM` <= " . $postObj->filters->GpCM->max;
        }
    }
    if (isset($postObj->filters->GpEI)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpEI->min)) {
            $strReq .= "`GpEI` >= " . $postObj->filters->GpEI->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpEI->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpEI` <= " . $postObj->filters->GpEI->max;
        }
    }
    //---------------------------------------------------
    if (isset($postObj->filters->GpTD)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpTD->min)) {
            $strReq .= "`GpTD` >= " . $postObj->filters->GpTD->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpTD->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpTD` <= " . $postObj->filters->GpTD->max;
        }
    }
    if (isset($postObj->filters->GpTP)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpTP->min)) {
            $strReq .= "`GpTP` >= " . $postObj->filters->GpTP->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpTP->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpTP` <= " . $postObj->filters->GpTP->max;
        }
    }
    if (isset($postObj->filters->GpTPL)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpTPL->min)) {
            $strReq .= "`GpTPL` >= " . $postObj->filters->GpTPL->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpTPL->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpTPL` <= " . $postObj->filters->GpTPL->max;
        }
    }
    if (isset($postObj->filters->GpPRJ)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpPRJ->min)) {
            $strReq .= "`GpPRJ` >= " . $postObj->filters->GpPRJ->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpPRJ->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpPRJ` <= " . $postObj->filters->GpPRJ->max;
        }
    }
    if (isset($postObj->filters->GpCMSer)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpCMSer->min)) {
            $strReq .= "`GpCMSer` >= " . $postObj->filters->GpCMSer->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpCMSer->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpCMSer` <= " . $postObj->filters->GpCMSer->max;
        }
    }
    if (isset($postObj->filters->GpEISer)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpEISer->min)) {
            $strReq .= "`GpEISer` >= " . $postObj->filters->GpEISer->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpEISer->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpEISer` <= " . $postObj->filters->GpEISer->max;
        }
    }
    if (isset($postObj->filters->GpTDSer)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpTDSer->min)) {
            $strReq .= "`GpTDSer` >= " . $postObj->filters->GpTDSer->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpTDSer->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpTDSer` <= " . $postObj->filters->GpTDSer->max;
        }
    }
    if (isset($postObj->filters->GpTPSer)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpTPSer->min)) {
            $strReq .= "`GpTPSer` >= " . $postObj->filters->GpTPSer->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpTPSer->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpTPSer` <= " . $postObj->filters->GpTPSer->max;
        }
    }
    if (isset($postObj->filters->GpTPLSer)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpTPLSer->min)) {
            $strReq .= "`GpTPLSer` >= " . $postObj->filters->GpTPLSer->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpTPLSer->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpTPLSer` <= " . $postObj->filters->GpTPLSer->max;
        }
    }
    if (isset($postObj->filters->GpPRJSer)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->GpPRJSer->min)) {
            $strReq .= "`GpPRJSer` >= " . $postObj->filters->GpPRJSer->min;
            $minSet = true;
        }
        if (isset($postObj->filters->GpPRJSer->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`GpPRJSer` <= " . $postObj->filters->GpPRJSer->max;
        }
    }
}

if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `code_ec` DESC ";
else
    $strReq .= " ORDER BY `code_ec`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0]=='00000') {
    if ($requete->rowCount() != 0) {
        foreach ($requete as $req) {
            $obj = new stdClass();
        
            $obj->code_ec = $req['code_ec'];
        
            $obj->annee = $req['annee'];
        
            $req['eff_prev'] == null ? null : $obj->eff_prev = $req['eff_prev'];
        
            $req['eff_reel'] == null ? null : $obj->eff_reel = $req['eff_reel'];
        
            $obj->GpCM = $req['GpCM'];
        
            $obj->GpEI = $req['GpEI'];
        
            $obj->GpTD = $req['GpTD'];
        
            $obj->GpTP = $req['GpTP'];
        
            $obj->GpTPL = $req['GpTPL'];
        
            $obj->GpPRJ = $req['GpPRJ'];
        
            $obj->GpCMSer = $req['GpCMSer'];
        
            $obj->GpEISer = $req['GpEISer'];
        
            $obj->GpTDSer = $req['GpTDSer'];
        
            $obj->GpTPSer = $req['GpTPSer'];
        
            $obj->GpTPLSer = $req['GpTPLSer'];
        
            $obj->GpPRJer = $req['GpPRJSer'];
        
            $enseignement->values[] = $obj;
        }
    }
}
else {
    $enseignement->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $enseignement->errors[] = $obj;
}

echo json_encode($enseignement);