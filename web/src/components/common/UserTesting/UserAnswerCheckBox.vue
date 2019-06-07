<template>
    <v-checkbox
        class="answer"
        v-model="isRight"
        color="success"
        :label="answer.text"
    ></v-checkbox>
</template>

<script>
export default {
    props:{
        answer: Object
    },
    data(){
        return{
            value: false
        }
    },
    computed:{
        isRight:{
            get(){
                return this.value;
            },
            set(value){
                this.value = value;
                this.$emit("change");
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

