<template>
    <main class="home">
        <div v-for="(team, teamId) in home.teams" :key="teamId" class="team">
            <img :src="team.finished ? '/img/on.png' : '/img/off.png'" :alt="team.finished ? 'on' : 'off'" />
            <h3>{{ team.name }}</h3>
            <span v-for="(member, index) in team.members" :key="`member${index}`" class="member">{{ member }}</span>
        </div>
    </main>
</template>

<script>
import { mapState } from 'vuex'

export default {
    name: "home",
    computed: {
        ...mapState(['home']),
    },
    mounted() {
        this.$store.dispatch('homeLoadTeams')
    }
}
</script>

<style lang="scss">
.home {
    display: grid;
    width: 90%;
    max-width: 1000px;
    grid-auto-flow: column;
    grid-template-columns: 1fr 1fr;

    .team {
        display: grid;
        justify-items: center;
        align-content: start;
        grid-gap: .5rem;

        h3 {
            margin: 1rem 0;
            font-size: 2rem;
        }
        img {
            width: 3rem;
        }
    }
}
</style>