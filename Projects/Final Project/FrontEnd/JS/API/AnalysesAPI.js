
async function Get(page,pagesize,name) {
    try{

        let request = `${API_URL}/Analyses?`;

        let params = {
            page: page,
            pageSize: pagesize,
            Name: name
        }

        let querystring = new URLSearchParams(params);

        let response = await fetch(request+querystring,
            {
                method: "GET"
            }
        );

        if(!response.ok)
        {
            throw new Error(response);
        }

        let jsonresponse = await response.json();

        return jsonresponse;

    } 
    catch (error){
        return null;
    }
}

async function Mostrar() {
    let page = document.getElementById("page");
    let pagesize = document.getElementById("pagesize");
    let name = document.getElementById("name");
    var response = await Get(page.value,pagesize.value,name.value);
    var div = document.getElementById("mostrar");
    div.innerHTML = "";
    if(response == null)
    {
        console.log("Fallo la peticion Get");
    }

    let p1 = document.createElement("p");
    let p2 = document.createElement("p");
    let p3 = document.createElement("p");
    let p4 = document.createElement("p");
    let ul = document.createElement("ul");

    p1.innerText = "page:"+ response.page;
    p2.innerText = "pagesize:"+response.pageSize;
    p3.innerText = "totalpages:"+response.totalPages;
    p4.innerText = "totalcount:"+response.totalCount;

    response.data.forEach(item => {
        let li = document.createElement("li");
        li.innerHTML = 
        `<p><strong>${item.name}</strong> - ${item.description}<p>`;
        ul.appendChild(li);
    });

    div.append(p1, p2, p3, p4, ul);
}


let button = document.getElementById("cargar");

button.addEventListener("click", () =>
{
Mostrar();
})


