function GetMy()
{
  var d = new Date();
document.getElementById("MonthYear").innerHTML=null;


  var button = document.createElement("input");
button.type = "button"; 
button.value = "<"; 
button.onclick = GetMy;
document.getElementById("MonthYear").appendChild(button);

var label = document.createElement("Label"); 
label.innerHTML =  d.getMonth()+"  "+d.getFullYear() ;
document.getElementById("MonthYear").appendChild(label);
 


  
  var button = document.createElement("input");
button.type = "button"; 
button.value = ">"; 
button.onclick = GetMy;
document.getElementById("MonthYear").appendChild(button);
}