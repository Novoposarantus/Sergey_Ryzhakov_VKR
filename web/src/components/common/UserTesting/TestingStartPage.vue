<template>
    <v-card class="card">
        <v-card-text class="text-center">
            <div cla v-if="showMessage" v-html="messageHtml"></div>
            <div class="button">
                <v-btn
                    color="success"
                    dark
                    large
                    @click="start">
                    Начать
                </v-btn>
            </div>
        </v-card-text>
    </v-card>
</template>

<script>
export default {
    props:{
        message: {
            type: String,
            default: null
        }
    },
    computed:{
        showMessage(){
            return this.message != null && this.message.length > 0;
        },
        messageHtml(){
            return this.message
                .split("\n")
                .join("<br>");
        }
    },
    methods:{
        start(){
            this.$emit("start");
        }
    }
}
</script>

<style scoped>
.card{
    padding: 1.5rem;
}
.text-center{
    text-align: center;
}
.button{
    margin-top: 2rem;
}
</style>
