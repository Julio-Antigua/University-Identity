<template>

        <Details></Details>

</template>

<script>
import Details from '../../components/View/DetailsSubjectComponent.vue'
export default{
    name: 'DetailsSubjectView',
    props:{
        msg:String
    },
    components:{
        Details
    }
}
</script>

<style>

</style>