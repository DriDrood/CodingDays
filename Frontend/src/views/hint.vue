<template>
    <main class="hint">
        <div class="picker">
            <span v-for="index in count" :key="`i-${index}`">{{ result[index - 1] ?? "_" }}</span>
            <button v-if="result.length < count" @click="select">Vlož {{ current }}</button>
            <button v-else @click="send">Odeslat</button>
            <button @click="reset">Reset</button>
        </div>
        <div class="response">
            <span v-if="hint.response.text">{{ hint.response.text }}</span>
            <img v-if="hint.response.imageUrl" :src="hint.response.imageUrl" alt="response" />
        </div>
    </main>
</template>

<script>
import { mapState } from 'vuex'

export default {
    name: "hint",
    props: {
        count: {
            type: Number,
            default: 8,
        },
    },
    data: () => ({
        result: [],
        current: 0,
    }),
    computed: {
        ...mapState(['hint'])
    },
    methods: {
        select() {
            if (this.result.length >= this.count)
                return;

            this.result.push(this.current);
        },
        reset() {
            this.result = [];
        },
        send() {
            this.$store.dispatch('hintSendCode', { code: this.result.join(".") })
        },
    },
    mounted() {
        setInterval(() => this.current = Math.floor(Math.random() * 10), 600);
    },
};
</script>

<style lang="scss">
.hint {
    .picker {
        display: grid;
        padding: 3rem;
        grid-auto-flow: column;
        align-content: center;
        align-items: center;
        justify-content: center;
        grid-gap: 1rem;
    }

    .response {
        display: grid;
        grid-gap: 1rem;
        grid-auto-flow: row;
        justify-items: center;
    }
}

@media only screen and (max-width: 600px) {
    .hint .picker {
        grid-auto-flow: row;
        grid-template-columns: repeat(8, 1fr);

        button:not(:last-child) {
            grid-column: 1/5;
        }

        button:last-child {
            grid-column: 5/9;
        }
    }
}
</style>
