<template>
  <main class="register">
    <div>
      V případě jakýchkoli dotazů pište na
      <a href="mailto:samlachman@centrum.cz">samlachman@centrum.cz</a>
    </div>
    <div>
      Zbývá {{ remains }} míst. Ve frontě je {{ queue }} lidí.
    </div>
    <form @submit="send" method="post" action="/api/register/register">
      <label for="name">Jméno:*</label>
      <input type="text" v-model="name" id="name" required />
      <label for="surname">Příjmení:*</label>
      <input type="text" v-model="surname" id="surname" required />
      <label for="birth">Datum narození:*</label>
      <input type="date" v-model="birth" id="birth" required />
      <label for="phone">Telefon:*</label>
      <input type="tel" v-model="phone" id="phone" required />
      <label for="email">E-mail:*</label>
      <input type="email" v-model="email" id="email" required />
      <label for="needNtb">Potřebuji půjčit notebook (nemám žádný použitelný):</label>
      <input type="checkbox" v-model="needNtb" id="needNtb" />
      <label for="level">Úroveň programování:</label>
      <select v-model="level" id="level">
        <option value="0">Nikdy jsem neprogramoval, ale zajímá mě to</option>
        <option value="1">Už jsem si hrál ve scratchi / kreslil se želvou</option>
        <option value="2"> Zvládám základy ve "skutečném" programovacím jazyku (if, for, while, funkce, ...)</option>
        <option value="3">Napsal jsem sám už několik složitějších programů (nad 500 řádků)</option>
        <option value="4">Už jsem měl i nějaké reálné projekty pro jiné lidi</option>
        <option value="5">Dělám to profesionálně, můžu to klidně učit</option>
      </select>
      <label for="languages">Programovací jazyky, ve kterých jsem napsal alespoň jeden program:</label>
      <input type="text" v-model="languages" id="languages" />
      <label for="note">Poznámka: <br />(např. diety, panický strach z plyšových medvídků, našel jsi tady nebo v pozvánce chybu, atd.)</label>
      <textarea v-model="note" id="note" cols="30" rows="3"></textarea>
      <!-- <label for="bonus">Odhalil jsem tohle schované pole:*</label>
        <input type="checkbox" id="bonus" name="bonus" value="true" /> -->
      <button class="primary">Odeslat</button>
    </form>
  </main>
</template>

<script>
export default {
  name: "register",
  data: () => ({
    name: '',
    surname: '',
    birth: null,
    phone: '',
    email: '',
    needNtb: false,
    level: 0,
    languages: '',
    note: '',
    bonus: null,
  }),
  computed: {
    remains() {
      if (this.$store.state.register.count == null)
        return "??";

      return Math.max(20 - this.$store.state.register.count, 0)
    },
    queue() {
      if (this.$store.state.register.count == null)
        return "??";

      return Math.max(this.$store.state.register.count - 20, 0)
    },
  },
  methods: {
    send(ev) {
      ev.preventDefault()

      const data = {
        name: this.name,
        surname: this.surname,
        birth: this.birth,
        phone: this.phone,
        email: this.email,
        needNtb: this.needNtb,
        level: this.level,
        languages: this.languages,
        note: this.note,
        bonus: document.getElementById('bonus')?.value ?? null,
      }
      this.$store.dispatch('registerSend', data)
    }
  },
  mounted() {
    this.$store.dispatch("registerCount")
  },
};
</script>

<style lang="scss">
.register {
  justify-items: center;

  div {
      margin-bottom: 1rem;
      max-width: 90%;
  }
} 
</style>