<template>
    <v-form ref="form" class="form">
        <v-container>
            <v-toolbar flat color="transparent">
                <v-toolbar-title>Назначения</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-btn  @click="addAssignments()"
                        v-if="showAdd"
                        color="primary" 
                        dark>
                    Добавить назначение
                </v-btn>
            </v-toolbar>
            <assignments-select
                v-if="showSelect"
                @close="closeSelect()">
            </assignments-select>
            <assignments-table></assignments-table>
        </v-container>
    </v-form>
</template>

<script>
import AssignmentsTable from "@/components/common/Assignments/AssignmentsTable.vue";
import AssignmentsSelect from "@/components/common/Assignments/AssignmentsSelect.vue";

export default {
    components:{
        AssignmentsTable,
        AssignmentsSelect
    },
    data(){
        return {
            showSelect: false
        }
    },
    computed:{
        showAdd(){
            return !this.showSelect;
        }
    },
    methods:{
        addAssignments(){
            this.showSelect = true;
        },
        closeSelect(){
            this.showSelect = false;
        }
    }
}
</script>

<style scoped>
.select-field{
    max-width: 250px;
    display: inline-block;
}
.d-flex{
    display: flex;
}
.form{
    padding: 2rem;
}
</style>
