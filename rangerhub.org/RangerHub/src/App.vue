<template>
    <header>
        <template v-if="logged">
            <div class="team">Tým: {{ name }}</div>
            <div class="temp">{{ temp }}</div>
        </template>
        <template v-else>
            <div class="error">{{ error }}</div>
            <form @submit="send">
                <input type="text" v-model="name" placeholder="Jméno týmu" />
                <input type="password" v-model="password" placeholder="Heslo týmu" />
                <button>Přihlásit</button>
            </form>
        </template>
    </header>
    <main>
        <h1>RangerHub</h1>
        <img src="under-construction.webp" alt="under-construction" />
    </main>
</template>

<script>
export default {
  name: 'app',
  data: () => ({
      error: null,
      name: '',
      password: '',
      temp: null,
      logged: false,
  }),
  methods: {
    async send(ev) {
        ev.preventDefault()
        
        const response = await fetch('http://weather.rangerhub.org/api/team/loginTeam', { method: 'POST', headers: { 'Content-type': 'application/json' }, body: JSON.stringify({ name: this.name, password: this.password }) })

        if (response.status == 200) {
            this.logged = true
            this.temp = '40 C'
        } else {
            this.error = 'Chyba'
        }
    }
  }
}
</script>

<style lang="scss">
* {
    padding: 0;
    margin: 0;
}
html, body {
    width: 100%;
    height: 100%;
    color: black;
    background-color: white;
}
#app {
    display: grid;
    grid-template-rows: 5rem 1fr;
}

header {
    display: grid;
    justify-items: center;
    align-content: center;
    grid-auto-flow: column;
    color: white;
    background-color: hsl(106, 64%, 34%);

    .error {
        width: 300px;
        color: red;
        font-weight: bold;
    }
    form {
        display: grid;
        grid-auto-flow: column;
        grid-gap: .5rem;
    }
    input, button {
        padding: .5rem 1rem;
        border: none;
        border-radius: 5px;
    }
    button {
        cursor: pointer;
    }

    .team {
        font-size: 1.5rem;
        font-weight: bold;
    }
}

main {
    display: grid;
    width: 100%;
    grid-template-rows: 1fr 2fr;
    justify-items: center;
    align-items: center;

    h1 {
        font-size: 5rem;
    }
}
</style>
