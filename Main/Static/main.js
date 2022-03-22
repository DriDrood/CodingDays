const WriteCount = async () => {
    const result = await fetch("/api/count")
    const body = await result.json()

    const countElement = document.getElementById("count");
    countElement.textContent = `Zbývá ${Math.max(20 - body, 0)} míst. Ve frontě je ${Math.max(body - 20, 0)} lidí.`
}
WriteCount()
