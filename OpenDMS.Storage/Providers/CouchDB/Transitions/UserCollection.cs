﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpenDMS.Storage.Providers.CouchDB.Transitions
{
    public class UserCollection
    {
        public UserCollection()
        {
        }

        public List<Security.User> Transition(Model.View view)
        {
            List<Security.User> users = new List<Security.User>();
            User transitionUser;
            Model.Document doc;
            JArray rows;
            int totalRows;

            totalRows = view["total_rows"].Value<int>();

            if (totalRows <= 0)
                return users;

            rows = (JArray)view["rows"];

            for (int i = 0; i < rows.Count; i++)
            {
                transitionUser = new User();
                doc = (Model.Document)rows[i]["key"];
                users.Add(transitionUser.Transition(doc));
            }

            return users;
        }

        public List<Model.Document> Transition(List<Security.User> users)
        {
            List<Model.Document> docs = new List<Model.Document>();
            User transitionUser;

            if (users.Count <= 0)
                return docs;

            for (int i = 0; i < users.Count; i++)
            {
                transitionUser = new User();
                docs.Add(transitionUser.Transition(users[i]));
            }

            return docs;
        }
    }
}
