<template>
    <form class="login" @submit="send">
        <input type="text" v-model="name" placeholder="Username" />
        <input type="password" v-model="password" placeholder="Password" />
        <button>Login</button>
    </form>
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