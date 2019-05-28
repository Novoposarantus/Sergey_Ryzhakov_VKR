<template>
    <div>
        <v-form>
            <v-container>
                <v-toolbar flat color="white">
                    <v-toolbar-title>{{title}}</v-toolbar-title>
                    <v-spacer></v-spacer>
                    <question-type-dialog></question-type-dialog>
                </v-toolbar>
                <v-text-field
                    v-model="question.text"
                    :rules="[v => !!v || 'Поле не моет быть пустым']"
                    label="Вопрос"
                    required
                ></v-text-field>
    
                <v-select
                    :items="types"
                    v-model="question.questionTypeId"
                    label="Тип"
                    class="select-field"
                ></v-select>
            </v-container>
        </v-form>
        
    </div>
</template>

<script>
import QuestionTypeDialog from "@/components/common/QuestionTypeDialog.vue";
export default {
    components:{
        QuestionTypeDialog
    },
    data(){
        return {
            question:{},
            types: []
        }
    },
    computed:{
        title(){
            if(this.$router.params && this.$router.params.id && this.$router.params.id > 0) {
                return "Редактирование вопроса."
            }
            return "Создание нового вопроса"
        },
        showSave(){
            return this.question.text && this.question.answers.length > 4;
        }
    },
    methods:{
        save(){

        },
        addType(){
            
        }
    },
    beforeMount(){
        this.question = {
            ...this.$store.getters["questionEdit/QUESTION"]
        }
        this.types = this.$store.getters["questionEdit/TYPES"].map(el=>({
            text: el.name,
            value: el.id
        }))
    }
}
</script>

<style scoped>
.select-field{
    max-width: 250px;
}
</style>
