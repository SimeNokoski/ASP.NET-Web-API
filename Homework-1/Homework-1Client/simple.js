let btn1 = document.getElementById("btn1")
let btn2 = document.getElementById("btn2")
let input1 = document.getElementById("input1")

let port = "7223"
let url = `https://localhost:${port}/api/users`;

let getAllUsers = async () => {
    let respons = await fetch(url)
    let data = await respons.json();
    console.log(data)
}

let getUserByIndex = async ()=> {
    let respons = await fetch(url + "/index/" + input1.value)
    let data = await respons.text();
    console.log(data)
}
 
btn1.addEventListener("click",getAllUsers);
btn2.addEventListener("click",getUserByIndex);

 
 