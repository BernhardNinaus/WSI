namespace WSI.WortFilter;

public interface IWortFilterService {
    string FilterWörter(string satz);
    bool EnthaltetWörter(string satz);
}

public interface IWortFilterService<T> : IWortFilterService where T : IWortAustauschService {
}

public interface IWortFilterService<T1, T2> : IWortFilterService<T1> 
    where T1 : IWortAustauschService 
    where T2 : IWortSucheService {
}
