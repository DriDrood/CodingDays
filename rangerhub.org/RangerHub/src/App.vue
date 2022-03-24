<template>
    <header>
        <template v-if="logged">
            <div class="team">Tým: {{ name }}</div>
            <div class="temp">Teplota: {{ temp }}</div>
            <div class="users">Počet uživatelů: {{ userCount }} </div>
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
import jwtDecode from 'jwt-decode'

export default {
  name: 'app',
  data: () => ({
      error: null,
      name: '',
      password: '',
      temp: null,
      userCount: null,
      token: null,
  }),
  computed: {
      logged() {
          return this.token != null
      }
  },
  methods: {
    async send(ev) {
        ev.preventDefault()
        
        // login
        const response = await fetch('https://weather.rangerhub.org/api/team/loginTeam', {
            method: 'POST',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify({ name: this.name, password: this.password }) })
        if (response.status != 200) {
            this.error = `Chyba: ${await response.text()}`
            return
        }

        const data = await response.json()
        const jwt = jwtDecode(data.token)
        this.token = jwt.nameid
        const teamSecret = this.token.substring(0, 4)

        // get temp
        const responseWeather = await fetch('https://weather.rangerhub.org/api/weatherForecast')
        if (responseWeather.status != 200) {
            this.error = `Chyba: ${await responseWeather.text()}`
            return
        }
        const dataWeather = await responseWeather.json()
        this.temp = dataWeather[0].temperatureC

        // get user count
        const responseUsers = await fetch(`https://users-${teamSecret}.rangerhub.org/api/statistic/getUserCount`)
        if (responseUsers.status != 200) {
            this.error = `Chyba: ${await responseUsers.text()}`
            return
        }
        const dataUsers = await responseUsers.json()
        this.userCount = dataUsers.count
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
