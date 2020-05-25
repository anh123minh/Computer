using System;
using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Computer.Common;
using Computer.Infrastructure.Core;
using Computer.Service;
using Computer.Infrastructure.Extensions;
using Computer.Models.Computer;

namespace Computer.Controllers
{
    [System.Web.Http.RoutePrefix("api/computer")]
    [System.Web.Http.Authorize]
    public class ComputerController : ApiControllerBase
    {
        private IComputerService _computerService;

        public ComputerController(IErrorService errorService, IComputerService computerService) :
            base(errorService)
        {
            this._computerService = computerService;
        }

        [System.Web.Http.Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listComputer = _computerService.GetAll();

                var listComputerVm = Mapper.Map<List<ComputerViewModel>>(listComputer);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listComputerVm);

                return response;
            });
        }

        [System.Web.Http.Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, ComputerViewModel computerVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Model.Models.Computer newComputer = new Model.Models.Computer();
                    newComputer.UpdateComputer(computerVm);

                    var computer = _computerService.Add(newComputer);
                    _computerService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, computer);
                }
                return response;
            });
        }

        [System.Web.Http.Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, ComputerViewModel computerVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var computerDb = _computerService.GetById(computerVm.ComputerId);
                    computerDb.UpdateComputer(computerVm);
                    _computerService.Update(computerDb);
                    _computerService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _computerService.Delete(id);
                    _computerService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        //public ActionResult Detail(int productId)
        //{
        //    var productModel = _productService.GetById(productId);
        //    var viewModel = Mapper.Map<Product, ProductViewModel>(productModel);

        //    var relatedProduct = _productService.GetReatedProducts(productId, 6);
        //    ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);

        //    List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(viewModel.MoreImages);
        //    ViewBag.MoreImages = listImages;

        //    ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_productService.GetListTagByProductId(productId));
        //    return View(viewModel);
        //}

        //public ActionResult ComputerPagingList(int id, int page = 1, string sort = "")
        //{
        //    int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
        //    int totalRow = 0;
        //    var productModel = _computerService.GetAllPaging(page, pageSize, out totalRow);
        //    var computerViewModel = Mapper.Map<IEnumerable<Model.Models.Computer>, IEnumerable<ComputerViewModel>>(productModel);
        //    int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

        //    var category = _computerService.GetById(id);
        //    ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);
        //    var paginationSet = new PaginationSet<ProductViewModel>()
        //    {
        //        Items = productViewModel,
        //        MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
        //        Page = page,
        //        TotalCount = totalRow,
        //        TotalPages = totalPage
        //    };

        //    return View(paginationSet);
        //}
        //public ActionResult Search(string keyword, int page = 1, string sort = "")
        //{
        //    int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
        //    int totalRow = 0;
        //    var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
        //    var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
        //    int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

        //    ViewBag.Keyword = keyword;
        //    var paginationSet = new PaginationSet<ProductViewModel>()
        //    {
        //        Items = productViewModel,
        //        MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
        //        Page = page,
        //        TotalCount = totalRow,
        //        TotalPages = totalPage
        //    };

        //    return View(paginationSet);
        //}
        //public ActionResult ListByTag(string tagId, int page = 1)
        //{
        //    int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
        //    int totalRow = 0;
        //    var productModel = _productService.GetListProductByTag(tagId, page, pageSize, out totalRow);
        //    var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
        //    int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

        //    ViewBag.Tag = Mapper.Map<Tag, TagViewModel>(_productService.GetTag(tagId));
        //    var paginationSet = new PaginationSet<ProductViewModel>()
        //    {
        //        Items = productViewModel,
        //        MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
        //        Page = page,
        //        TotalCount = totalRow,
        //        TotalPages = totalPage
        //    };

        //    return View(paginationSet);
        //}
        //public JsonResult GetListProductByName(string keyword)
        //{
        //    var model = _productService.GetListProductByName(keyword);
        //    return Json(new
        //    {
        //        data = model
        //    }, JsonRequestBehavior.AllowGet);
        //}
    }
}