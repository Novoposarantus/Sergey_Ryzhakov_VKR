<template>
      <v-layout row wrap>
        <v-flex shrink pa-1>
            <v-checkbox
                v-model="isRight"
                color="success"
            ></v-checkbox>
        </v-flex>
        <v-flex grow pa-1>
            <v-text-field
                v-model="text"
                :rules="[v => !!v || 'Поле не моет быть пустым' ]"
                label="Ответ"
                required
            ></v-text-field>
        </v-flex>
        <v-flex shrink pa-1>
            <v-btn 
                v-if="showDelete"
                color="red" 
                dark 
                @click="deleteAnswer()">
                    Удалить
                    <v-icon dark right>far fa-trash-alt</v-icon>
            </v-btn>
        </v-flex>
      </v-layout>
</template>

<script>
export default {
    props: {
        value: Object,
        showDelete: Boolean
    },
    computed:{
        isRight:{
            get(){
                return this.value.isRight;
            },
            set(newIsRight){
                let newValue = {
                    ...this.value,
                    isRight: newIsRight
                };
                this.$emit("change", {oldValue : this.value, newValue});
            }
        },
        text:{
           get(){
                return this.value.text;
            },
            set(newText){
                let newValue = {
                    ...this.value,
                    text: newText
                };
                this.$emit("change", {oldValue : this.value, newValue});
            } 
        }
    },
    methods:{
        deleteAnswer(){
            this.$emit("delete", this.value);
        }
    },
}
</script>

