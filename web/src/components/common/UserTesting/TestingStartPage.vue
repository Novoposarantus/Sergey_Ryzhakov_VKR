<template>
    <v-card class="card">
        <v-card-text class="text-center">
            Предупреждающий текст
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
