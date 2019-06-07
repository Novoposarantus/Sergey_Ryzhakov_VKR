<template>
    <v-radio-group v-model="isRight" :mandatory="false">
        <v-radio 
            class="answer"
            v-for="answer in answers"
            :key="answer.id"
            :value="answer.id"
            :label="answer.text"/>
    </v-radio-group>
</template>

<script>
export default {
    props:{
        answers: Array
    },
    data(){
        return{
            value: null
        }
    },
    computed:{
        isRight:{
            get(){
                return this.value;
            },
            set(value){
                if(this.value != null){
                    this.$emit("change", this.value);
                }
                this.$emit("change", value);
                this.value = value;
            }
        }
    }
}
</script>

<style scoped>
.answer{
    margin: 5px 0;
}
</style>
