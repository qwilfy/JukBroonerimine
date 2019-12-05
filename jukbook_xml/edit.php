<?php
$filexml = simplexml_load_file('juksbron.xml');

if(isset($_POST['submitSave'])) {

	foreach($filexml -> inimene as $inim){
		if($inim['id']==$_POST['id']){
			$inim-> makstud = $_POST['price'];
			break;
		}
	}
	file_put_contents('juksbron.xml', $filexml->asXML());
	header('location:index.php');
}

foreach($filexml -> inimene as $inim){
	if($inim['id']==$_GET['id']){
		$id = $inim['id'];
		$price = $inim->makstud;
		break;
	}
}

?>
<form method="post">
	<table cellpadding="2" cellspacing="2">
		<tr>
			<td>Id</td>
			<td><input type="hidden" name="id" value="<?php echo $id; ?>" readonly="readonly"></td>
		</tr>
		<tr>
			<td>makstud</td>
			<td><select name="price" value="<?php echo $price; ?>">
			<option value="Jah">Jah</option>
			<option value="Ei">Ei</option>
			</select></td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td><input type="submit" value="Save" name="submitSave"></td>
		</tr>
	</table>
</form>