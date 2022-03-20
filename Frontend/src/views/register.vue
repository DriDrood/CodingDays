<template>
  <div class="register">
    <h1>Coding days</h1>
    <router-link :to="{ name: 'home' }">Zpět</router-link>
    <div>
      V případě jakýchkoli dotazů pište na
      <a href="mailto:samlachman@centrum.cz">samlachman@centrum.cz</a>
    </div>
    <div>
      Zbývá {{ remains }} míst. Ve frontě je {{ queue }} lidí.
    </div>
    <form @submit="send" method="post" action="/api/register">
      <label for="name">Jméno:*</label>
      <input type="text" name="name" id="name" required />
      <label for="surname">Příjmení:*</label>
      <input type="text" name="surname" id="surname" required />
      <label for="birth">Datum narození:*</label>
      <input type="date" name="birth" id="birth" required />
      <label for="phone">Telefon:*</label>
      <input type="tel" name="phone" id="phone" required />
      <label for="email">E-mail:*</label>
      <input type="email" name="email" id="email" required />
      <label for="needNtb">Potřebuji půjčit notebook (nemám žádný použitelný):</label>
      <input type="checkbox" name="needNtb" id="needNtb" />
      <label for="level">Úroveň programování:</label>
      <select name="level" id="level">
        <option value="0">Nikdy jsem neprogramoval, ale zajímá mě to</option>
        <option value="1">Už jsem si hrál ve scratchi / kreslil se želvou</option>
        <option value="2"> Zvládám základy ve "skutečném" programovacím jazyku (if, for, while, funkce, ...)</option>
        <option value="3">Napsal jsem sám už několik složitějších programů (nad 500 řádků)</option>
        <option value="4">Už jsem měl i nějaké reálné projekty pro jiné lidi</option>
        <option value="5">Dělám to profesionálně, můžu to klidně učit</option>
      </select>
      <label for="languages">Programovací jazyky, ve kterých jsem napsal alespoň jeden program:</label>
      <input type="text" name="languages" id="languages" />
      <label for="note">Poznámka: <br />(např. diety, panický strach z plyšových medvídků, našel jsi tady nebo v pozvánce chybu, atd.)</label>
      <textarea name="note" id="note" cols="30" rows="3"></textarea>
      <!-- <label for="bonus">Odhalil jsem tohle schované pole:*</label>
        <input type="checkbox" id="bonus" name="bonus" value="true" /> -->
      <button>Odeslat</button>
    </form>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "register",
  computed: {
    ...mapState([ "registerCount" ]),
    remains() {
      if (this.registerCount == null)
        return "??";

      return Math.max(20 - this.registerCount, 0)
    },
    queue() {
      if (this.registerCount == null)
        return "??";

      return Math.max(this.registerCount - 20, 0)
    },
  },
  methods: {
    send(ev) {
      ev.preventDefault()


    }
  },
  mounted() {
    this.$store.dispatch("getRegisterCount")
  },
};
</script>

<style>
.register div, .register a {
    margin-bottom: 1rem;
    max-width: 90%;
}
</style>