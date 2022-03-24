<template>
    <form class="login" @submit="send">
        <input type="text" v-model="name" placeholder="Username" />
        <input type="password" v-model="password" placeholder="Password" />
        <button>Login</button>
        <span @click="forgot">Forgot password</span>
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
        },
        forgot() {
            this.$store.dispatch('forgot', { name: this.name })
        }
    }
}
</script>

<style lang="scss">
form {
    display: grid;
    padding: 1rem 2rem;
    grid-gap: .5rem;

    border-radius: 1rem;
    box-shadow: 2px 2px 5px hsl(0, 0%, 74%);
    background-color: hsl(0, 0%, 94%);

    input, button {
        padding: .5rem 1rem;
        border: none;
        border-radius: .3rem;
    }
    button {
        cursor: pointer;
        background-color: hsl(0, 0%, 74%);
    }

    span {
        cursor: pointer;
        transition: .3s color;

        &:hover {
            color: hsl(0, 0%, 30%);
        }
    }
}
</style>