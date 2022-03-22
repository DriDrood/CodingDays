<template>
    <header>
        <h1>Coding days</h1>
        <menu>
            <li><router-link :to="{ name: 'home' }">Domů</router-link></li>
            <li><router-link :to="{ name: 'invitation' }">Pozvánka / informace</router-link></li>
            <li><router-link :to="{ name: 'register' }">Registrace</router-link></li>
            <li><router-link :to="{ name: 'hint' }">Nápovědy</router-link></li>
        </menu>
    </header>
    <notifications />
    <router-view />
</template>

<script>
import Notifications from './components/notifications.vue'

export default {
    name: "App",
    components: {
        Notifications
    },
};
</script>

<style lang="scss">
/* reset */
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;

    font-family: "VT323", monospace;
    font-size: 1.2rem;
}

html, body, #app {
    width: 100%;
    height: 100%;
}

/* custom elements */
h1 {
    margin-bottom: 2rem;
    justify-self: center;
    align-self: end;
    font-size: 4rem;
}
h2 {
    font-size: 1.5rem;
    align-self: start;
    font-weight: normal;
}
p {
    text-align: center;
}

a {
    color: hsl(0, 0%, 70%);
    transition: color 0.5s;
}
a:hover {
    color: hsl(0, 0%, 90%);
}

/* main */
#app {
    display: grid;
    justify-items: center;
    align-content: start;

    padding: 1rem;
    overflow-y: scroll;

    color: white;
    background-color: hsl(0, 0%, 20%);
    
    header {
        display: grid;
        margin-bottom: 3rem;
        justify-items: center;

        menu {
            display: grid;
            grid-auto-flow: column;
            grid-gap: 1rem;

            li {
                display: inline-block;
            }
        }
    }
}

/* inputs */
input,
textarea,
select,
button {
    margin-top: 0.2rem;
    margin-bottom: 0.7rem;
    padding: 0.5rem 1rem;

    border: none;
    border-radius: 0.3rem;
    background-color: hsl(0, 0%, 80%);
    outline: none;

    transition: background-color 0.5s;
}
input:focus,
input:focus:hover,
textarea:focus,
textarea:focus:hover,
select:focus,
select:focus:hover {
    background-color: hsl(0, 0%, 60%);
}
input:hover,
textarea:hover,
select:hover {
    background-color: hsl(0, 0%, 90%);
}

button {
    cursor: pointer;

    background-color: hsl(0, 0%, 57%);
    &:hover {
        background-color: hsl(0, 0%, 70%);
    }

    &.primary {
        background-color: hsl(101, 43%, 57%);
        &:hover {
            background-color: hsl(101, 43%, 70%);
        }
    }

    &.danger {
        background-color: hsl(0, 43%, 57%);
        &:hover {
            background-color: hsl(0, 43%, 70%);
        }
    }
}

select {
    width: 100%;
    white-space: pre-wrap;
    text-overflow: ellipsis;
}

/* checkbox */
input[type="checkbox"] {
    position: relative;

    width: 1rem;
    height: 1rem;
    margin-right: 0.75rem;
    padding: 0;
    border-radius: 0;

    cursor: pointer;
    -webkit-appearance: none;
    -moz-appearance: none;
    appearance: none;
    outline: 0;
}
input[type="checkbox"]:checked {
    background-color: hsla(0, 0%, 20%, 0);
}
input[type="checkbox"]::before {
    content: "";

    position: absolute;
    left: 0;
    top: 0;
    z-index: 1;

    width: 100%;
    height: 100%;

    transition: all 0.3s ease-in-out;
}
input[type="checkbox"]:checked::before {
    height: 50%;

    transform: rotate(-45deg);

    border-width: 4px;
    border-color: hsl(101, 43%, 57%);
    border-bottom-style: solid;
    border-left-style: solid;
}
</style>
