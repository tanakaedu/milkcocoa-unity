var milkcocoa = new MilkCocoa("https://io-ji3ay1vq5.mlkcca.com");
/* your-app-id にアプリ作成時に発行される"io-"から始まるapp-idを記入します */
var chatDataStore = milkcocoa.dataStore("chat");
var textArea, board, textName, isLogin;
window.onload = function(){
  textArea = document.getElementById("msg");
  board = document.getElementById("board");
  textName = document.getElementById("nm");
  isLogin = false;
}

function cbCurrentUser(err,user) {
  if (err != null) {
    alert("error:"+err.toString());
  }
  else {
    alert("userid="+user.id);
  }
}

function clickEvent(){
  if (!isLogin) {
    isLogin = true;
    document.getElementById("btn").textContent = "send message!";
    // Unityのloginを呼び出す
    u.getUnity().SendMessage("CommMilkcocoa","login",textName.value);
  }

  var text = textArea.value;
  sendText(text);
}

function sendText(text){
  chatDataStore.push({message : text},function(data){
    console.log("送信完了!");
    textArea.value = "";
  });
}

chatDataStore.on("push",function(data){
  addText(data.value.message);
});

function addText(text){
  var msgDom = document.createElement("li");
  msgDom.innerHTML = text;
  board.insertBefore(msgDom, board.firstChild);
}

function login_ok() {
}

function login_error() {
  isLogin = false;
  document.getElementById("btn").textContent = "login";
  alert("同じ名前のユーザーがすでにいます。");
}

