<template>
    <main class="login">
        <form @submit="send" method="POST" action="/api/team/login">
            <label for="name">Jméno:*</label>
            <input type="text" v-model="name" id="name" />
            <label for="password">Heslo:*</label>
            <input type="password" v-model="password" id="password" />
            <button class="primary">Přihlásit</button>
        </form>
    </main>
</template>

<script>
export default {
    name: 'login',
    data: () => ({
        name: '',
        password: '',
    }),
    methods: {
        send(ev) {
            ev.preventDefault()
            this.$store.dispatch('login', { name: this.name, password: this.password })
        }
    }
}
</script>

<style lang="scss">
.login {
  justify-items: center;
} 
</style>