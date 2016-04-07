﻿namespace MyTested.Mvc.Builders.Base
{
    using System.Collections.Generic;
    using Builders.ShouldPassFor;
    using Contracts.Base;
    using Contracts.ShouldPassFor;
    using Exceptions;
    using Internal.TestContexts;
    using Utilities.Extensions;

    /// <summary>
    /// Base class for all test builders with action call.
    /// </summary>
    public abstract class BaseTestBuilderWithAction : BaseTestBuilderWithController, IBaseTestBuilderWithAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithAction" /> class.
        /// </summary>
        protected BaseTestBuilderWithAction(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <value>Action name to be tested.</value>
        internal string ActionName => this.TestContext.ActionName;

        internal IEnumerable<object> ActionLevelAttributes => this.TestContext.ActionAttributes;
        
        public new IShouldPassForTestBuilderWithAction ShouldPassFor()
        {
            return new ShouldPassForTestBuilderWithAction(this.TestContext);
        }

        /// <summary>
        /// Tests whether the tested action's model state is valid.
        /// </summary>
        protected void CheckValidModelState()
        {
            if (!this.TestContext.ModelState.IsValid)
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have valid model state with no errors, but it had some.",
                    this.ActionName,
                    this.Controller.GetName()));
            }
        }
    }
}
