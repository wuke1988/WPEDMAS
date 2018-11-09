using Catel.Fody;
using Catel.IoC;
using Orc.FilterBuilder;
using Orc.FilterBuilder.Models;
using Orc.FilterBuilder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPEDMAS.Data;

namespace WPEDMAS.QueryClient.Providers
{
    public abstract class PredefinedSchemeNode
    {
        public string PropertyName { get; [param:NotNull]set; }
        internal abstract PropertyExpression Export([NotNull] ConditionGroup group, [NotNull] IPropertyCollection propertyCollection);
    }
    public class PredefinedCondition : PredefinedSchemeNode
    {
        public Condition Condition { get; set; }
        public object Value { get; set; }
        internal override PropertyExpression Export([NotNull] ConditionGroup parent, [NotNull] IPropertyCollection propertyCollection)
        {
            var property = propertyCollection.GetProperty(PropertyName);
            var expression = new PropertyExpression
            {
                Parent = parent,
                Property = property
            };
            var dataExpr = expression.DataTypeExpression;
            dataExpr.SelectedCondition = Condition;
            if (Value != null)
            {
                var valueProperty = dataExpr.GetType().GetProperty("Value");
                valueProperty.SetValue(dataExpr,ConvertEx.SafeCastValue(Value,valueProperty.PropertyType));
            }
            return expression;
        }
    }
    public class PredefinedNestedScheme : PredefinedSchemeNode
    {
        public ObservableCollection<PredefinedSchemeNode> Conditions { get; set; }
        public PredefinedNestedScheme()
        {
            Conditions = new ObservableCollection<PredefinedSchemeNode>();
        }
        internal override PropertyExpression Export([NotNull] ConditionGroup parent, [NotNull] IPropertyCollection propertyCollection)
        {
            var property = propertyCollection.GetProperty(PropertyName);
            var propertyExpression = new PropertyExpression
            {
                Parent = parent,
                Property = property
            };
            var dataTypeExpression = propertyExpression.DataTypeExpression;
            dataTypeExpression.SelectedCondition = Condition.Matches;


            var reflectionService = this.GetDependencyResolver().Resolve<IReflectionService>();
            var properties = reflectionService.GetInstanceProperties(property.Type);
            var group = new ConditionGroup()
            {
                Type=ConditionGroupType.And
            };
            Conditions.Select(o => o.Export(group, properties)).Sink(group.Items.Add);
            
            ((ComplexTypeExpression)dataTypeExpression).Value = new FilterScheme(property.Type, "Default", group);
            return propertyExpression;
        }
    }
    public class PredefinedScheme
    {
        public ObservableCollection<PredefinedSchemeNode> Conditions { get; set; }

        public PredefinedScheme()
        {
            Conditions = new ObservableCollection<PredefinedSchemeNode>();
        }
        public PredefinedScheme(IEnumerable<PredefinedSchemeNode> conditions)
        {
            Conditions = new ObservableCollection<PredefinedSchemeNode>(conditions);
        }
        public FilterScheme Export(Type entityType)
        {
            var reflectionService = this.GetDependencyResolver().Resolve<IReflectionService>();
            var properties = reflectionService.GetInstanceProperties(entityType);
            var group = new ConditionGroup()
            {
                Type= ConditionGroupType.And
            };
            Conditions.Select(o=>o.Export(group,properties)).Sink(group.Items.Add);
            var scheme = new FilterScheme(entityType,"Default",group);
            return scheme;
        }
    }
}
