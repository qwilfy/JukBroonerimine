<?php
$filexml = new DOMDocument;
$filexml = simplexml_load_file("juksbron.xml");
error_reporting(E_ERROR | E_WARNING | E_PARSE);

$count = 0;
$id = 0;

function searchPeopleByName($query){
    global $filexml;
    $result = array();
    foreach ($filexml -> inimene as $innime){
        if (substr(strtolower($innime -> nimi), 0, strlen($query))==strtolower($query))
            array_push($result, $innime);
    }
    return $result;
}
function searchPeopleByDate($query){
    global $filexml;
    $result = array();
    foreach ($filexml -> inimene as $innime){
        if (substr(strtolower($innime -> telefoninr), 0, strlen($query))==strtolower($query))
            array_push($result, $innime);
    }
    return $result;
}
if(isset($_POST['submitSave'])) {

	foreach($filexml -> inimene as $inim){
		if($inim['id']==$_POST['id']){
			$inim-> makstud = $_POST['price'];
			break;
		}
	}
	file_put_contents('juksbron.xml', $filexml->asXML());
}

if(isset($_GET['id'])){
    foreach($filexml -> inimene as $inim){
	    if($inim['id']==$_GET['id']){
		    $id = $inim['id'];
		    $price = $inim->makstud;
		    break;
	    }
      }
      }
?>
<!DOCTYPE html>
<html>
    <head>
        <title>Admin Panel</title>
        <meta charset="utf-8">
      <link rel="stylesheet" type="text/css" href="design.css">
        <style>
            body{
                border: 6px inset rgba(28,110,164,0.46);
                border-radius: 21px;
                    margin: 0;
                position: absolute;
                top: 50%;
                left: 50%;
                margin-right: -50%;
                transform: translate(-50%, -50%) }
            }
            table.greyGridTable {
              border: 2px solid #FFFFFF;
              width: 100%;
              text-align: center;
              border-collapse: collapse;
            }
            table.greyGridTable td, table.greyGridTable th {
              border: 1px solid #FFFFFF;
              padding: 3px 4px;
            }
            table.greyGridTable tbody td {
              font-size: 13px;
            }
            table.greyGridTable td:nth-child(even) {
              background: #EBEBEB;
            }
            table.greyGridTable thead {
              background: #FFFFFF;
              border-bottom: 4px solid #333333;
            }
            table.greyGridTable thead th {
              font-size: 15px;
              font-weight: bold;
              color: #333333;
              text-align: center;
              border-left: 2px solid #333333;
            }
            table.greyGridTable thead th:first-child {
              border-left: none;
            }

            table.greyGridTable tfoot {
              font-size: 14px;
              font-weight: bold;
              color: #333333;
              border-top: 4px solid #333333;
            }
            table.greyGridTable tfoot td {
              font-size: 14px;
            }
            h1{
                font-family: Verdana, Geneva, sans-serif;
                    font-size: 21px;
                    letter-spacing: -1.6px;
                    word-spacing: 1.2px;
                    color: #000000;
                    font-weight: 400;
                    text-decoration: none solid rgb(68, 68, 68);
                    font-style: italic;
                    font-variant: small-caps;
                    text-transform: uppercase;
                    display: flex;
                        margin: 0;
                position: absolute;
                top: -10%;
                left: 50%;
                margin-right: -50%;
                transform: translate(-50%, -50%)
            }
        </style>
    </head>
    <body>
        <h1>Ilusalong Admin Panel</h1>
        <table class="greyGridTable">
            <tr>
                <th>Nimi</th>
                <th>Telefoninr</th>
                <th>Kellaaeg</th>
                <th>Teenus</th>
                <th>Makstud</th>
            </tr>
            <?php
          
            foreach($filexml -> inimene as $inim) {
                echo "<tr>";
                echo "<td>".($inim -> nimi)."</td>";
                echo "<td>".($inim -> telefoninr)."</td>";
                echo "<td>".($inim -> kellaaeg)."</td>";
                echo "<td>".($inim -> teenus)."</td>";
                echo "<td>".($inim -> makstud)."</td>";
                echo "</tr>";
                }
            
            ?>
        </table>

        

        <br />
        <form action="?" method="post">
            Otsi klient: <input type="text" name="search" placeholder="Nimi"/>
            <input type="submit" value="Otsi" />
        </form>
        <table class="greyGridTable">
            <tr>
                <th>Nimi</th>
                <th>Telefoninr</th>
                <th>Kellaaeg</th>
                <th>Teenus</th>
                <th>Makstud</th>
            </tr>
            <?php
            if(!empty($_POST["search"])){
            $result = searchPeopleByName($_POST["search"]);
            foreach($result as $inim) {
            $count++;
                echo "<tr>";
                echo "<td>".($inim -> nimi)."</td>";
                echo "<td>".($inim -> telefoninr)."</td>";
                echo "<td>".($inim -> kellaaeg)."</td>";
                echo "<td>".($inim -> teenus)."</td>";
                echo "<td>".($inim -> makstud)."</td>";
                echo "</tr>";
                echo "<h4>".( $count) .": Otsingutulemus </h4>";
                }
            }
            ?>
        </table>




         <br />
        <form action="?" method="post">
            Otsi kliendi telefoninr: <input type="text" name="search" placeholder="Telefoninr"/>
            <input type="submit" value="Otsi" />
        </form>
        <table class="greyGridTable">
          <thead>
         <tr>
                <th>Nimi</th>
                <th>Telefoninr</th>
                <th>Kellaaeg</th>
                <th>Teenus</th>
                <th>Makstud</th>
            </tr>
              </thead>
            <tbody>
            <?php
            if(!empty($_POST["search"])){
            $result = searchPeopleByDate($_POST["search"]);
            foreach($result as $inim) {
                echo "<tr>";
                echo "<td>".($inim -> nimi)."</td>";
                echo "<td>".($inim -> telefoninr)."</td>";
                echo "<td>".($inim -> kellaaeg)."</td>";
                echo "<td>".($inim -> teenus)."</td>";
                echo "<td>".($inim -> makstud)."</td>";
                echo "</tr>";
                }
            }
            ?>
                </tbody>
        </table>
		<br>
			<table class="greyGridTable">
			<tr>
                <th>Nimi</th>
                <th>Telefoninr</th>
                <th>Kellaaeg</th>
                <th>Teenus</th>
                <th>Makstud</th>
            </tr>
			<?php
            foreach($filexml -> inimene as $inim) {
                if ($inim -> attributes() == $_GET['id']){
                echo "<tr>";
                echo "<td bgcolor='#7d7fa1'>".($inim -> nimi)."</td>";
                echo "<td>".($inim -> telefoninr)."</td>";
                echo "<td bgcolor='#7d7fa1'>".($inim -> kellaaeg)."</td>";
                echo "<td>".($inim -> teenus)."</td>";
                echo "<td bgcolor='#7d7fa1'>".($inim -> makstud)."</td>";
                }
                else{
                echo "<tr>";
                echo "<td>".($inim -> nimi)."</td>";
                echo "<td>".($inim -> telefoninr)."</td>";
                echo "<td>".($inim -> kellaaeg)."</td>";
                echo "<td>".($inim -> teenus)."</td>";
                echo "<td>".($inim -> makstud)."</td>";
                }
				?>
				<td><a href="index.php?id=<?php echo $inim['id']; ?> ">Muuda</a></td>
                <?php
				echo "</tr>";
				}
				?>
        </table>
        <form method="post">
        <input type="hidden" name="id" value="<?php echo $id; ?>" readonly="readonly">
            <?php 
            if ($id == 0){
            ?>
            <?php 
            }
            else{
            
            ?>
            <select name="price" value="<?php echo $price; ?>">
			<option value="Jah">Jah</option>
			<option value="Ei">Ei</option>
			</select>
            &nbsp;
            <input type="submit" value="Muuda" name="submitSave">
            <?php }?>
        </form>
    </body>
</html>