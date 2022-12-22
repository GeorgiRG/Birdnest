let timerId = setInterval(() => getItems(), 2000);
const dataTable = document.getElementById("dataTable")
//canvas variables
let amount = 10;
const canvas = document.getElementById('coordinatePlot')
const context = canvas.getContext('2d')
let radius = 0
let centerX = 0
let centerY = 0


const getItems = () =>
{
    fetch('/api/ViolationsData')
        .then(response => response.json())
        .then(data => createTable(data))
        .catch(error => console.error('Unable to get items.', error));
}

const createTable = (data) =>
{
    try
    {
        drawCircle()
        console.log(data)
        //table columns in order - PilotID, FirstName, LastName, Email, PhoneNumber,
        //Time, Distance, Duration, ViolationLocationX, ViolationLocationY
        let rowsHtml = "<thead> <tr> " + "<th> PilotID </th> " + "<th> First Name </th> "
                            + "<th> Last Name </th> " + "<th> Email </th> "
                            + "<th> Phone Number </th> " + "<th> Last Detected At </th> "
                            + "<th> Closest Distance </th> " + "<th> Overall Duration (sec) </th> "
                            + "<th> X coordinates </th> " + "<th> Y coordinates </th> " + "</tr> </thead>"

        rowsHtml += "<tbody>"
        data.forEach(function (entry, i)
        {

            rowsHtml += `<tr> <td> ${entry.pilotID} </td> <td> ${entry.firstName} </td> <td> ${entry.lastName} </td>`
                + `<td> ${entry.email} </td> <td> ${entry.phoneNumber} </td> <td> ${entry.time} </td>`
                + `<td> ${entry.distance / 1000}m </td> <td> ${entry.duration}s </td> `
                + `<td> ${entry.violationLocationX} </td> <td> ${entry.violationLocationY} </td> </tr>`

            if (i < amount)
            {
                drawViolations(entry.violationLocationX, entry.violationLocationY, entry.distance, entry.pilotID)
            }
        })
        rowsHtml += "</tbody>"
        dataTable.innerHTML = rowsHtml

    }
    catch (error)
    {
        console.log(error)
    }
}

const drawViolations = (posX, posY, distance, id) =>
{
    //find the ratio of current radius to real unit radius
    //converting values to relate to X,Y(100, 100)
    //we get pixel/meter ratio as ratioM
    let ratioM = radius / 100000
    let violationX = ((posX - 250000) * ratioM) + centerX
    let violationY = ((posY - 250000) * ratioM) + centerY
    let violationText = `${id}, ${distance / 1000}m`
    console.log(violationX, violationY, radius)

    //draw
    context.beginPath();
    context.lineWidth = 1
    context.strokeStyle = '#FFC300'
    context.fillStyle = 'orange'
    context.font = `${radius / 30}px Arial`
    context.fillText(violationText, violationX + 5, violationY + 10)
    context.arc(violationX, violationY, 3, 0, 2 * Math.PI, false)
    context.fill()
    context.stroke()


}

const drawCircle = () =>
{
    //sensor is drawn with radius equal to shortest side
    //so to match that next is at 100,100 instead of 250, 250
    canvas.width = window.innerWidth / 1.5
    canvas.height = window.innerHeight / 1.5
    centerX = canvas.width / 2
    centerY = canvas.height / 2
    if (centerY > centerX) {
        radius = centerX
    }
    else
    {
        radius = centerY
    }
    //restricted area
    context.beginPath();
    context.arc(centerX, centerY, radius, 0, 2 * Math.PI, false)
    context.lineWidth = 3
    context.strokeStyle = '#ef3a14'
    context.stroke()

    //center annotation
    context.beginPath();
    context.lineWidth = 1   
    context.font = `${radius / 15}px Arial`
    context.fillText("Nest", centerX + 5, centerY + 10)
    context.arc(centerX, centerY, 3, 0, 2 * Math.PI, false)
    context.strokeStyle = '#145aef'
    context.fillStyle = 'blue'
    context.fill()
    context.stroke()
}

const getAmount = () =>
{
    let txtInput = document.getElementById("amount")
    amount = txtInput.value

}